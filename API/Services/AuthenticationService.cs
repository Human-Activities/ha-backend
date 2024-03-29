﻿using DAL.UnitOfWork;
using DAL;
using DAL.DataEntities;
using API.Authenticators;
using Services.TokenValidators;
using Services;
using Services.PasswordHasher;
using API.Exceptions;
using API.Models.Authentication;
using DAL.CommonVariables;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;

namespace API.Services
{
    public class AuthenticationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly Authenticator _authenticator;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly AccessTokenValidator _accessTokenValidator;

        public AuthenticationService(IConfiguration configuration, Authenticator authenticator)
        {
            _configuration = configuration;
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
            _passwordHasher = ServicesFactory.CreateBCryptPasswordHasher();
            _authenticator = authenticator;
            _refreshTokenValidator = ServicesFactory.CreateRefreshTokenValidator(_configuration);
            _accessTokenValidator = ServicesFactory.CreateAccessTokenValidator(_configuration);
        }

        public async Task<RegisterResult> Register(RegisterRequest request)
        {
            User? user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Login == request.Login);
            if (user != null)
                throw new OperationException(StatusCodes.Status400BadRequest, "User with that login recently exists.");

            var salt = _passwordHasher.GenerateSalt();

            var userIdentity = await _uow.UserIdentityRepo.SingleOrDefaultAsync(ui => ui.Salt == salt);
            if (userIdentity != null)
            {
                while (userIdentity.Salt == salt)
                {
                    salt = _passwordHasher.GenerateSalt();
                    userIdentity = await _uow.UserIdentityRepo.SingleOrDefaultAsync(ui => ui.Salt == salt);
                    if (userIdentity == null)
                    {
                        break;
                    }
                }
            }

            var pepper = _configuration.GetValue<string>("PasswordSettings:Pepper");
            var passwordHash = _passwordHasher.Hash(request.Password + pepper, salt);

            var newUser = new User()
            {
                Name = request.Name,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Login = request.Login,
                PasswordHash = passwordHash,
                RoleId = 2 //(int)RoleType.LoggedUser
            };

            try
            {
                await _uow.UserRepo.AddAsync(newUser);
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new OperationException(StatusCodes.Status500InternalServerError, ex.Message);
            }

            var createdUser = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Login == request.Login);

            UserIdentity createdUserIdentity = new UserIdentity()
            {
                UserId = createdUser.Id,
                UserGuid = createdUser.UserGuid,
                Salt = salt
            };

            try
            {
                await _uow.UserIdentityRepo.AddAsync(createdUserIdentity);
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new OperationException(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return new RegisterResult { IsSuccess = true, Message = "Poprawnie utworzono użytkownika." };
        }

        public async Task<LoginResult> Login(LoginRequest request)
        {
            if (request == null)
                throw new OperationException(StatusCodes.Status400BadRequest, "Invalid client request.");

            var user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Login == request.Login);

            if (user == null)
                throw new OperationException(StatusCodes.Status400BadRequest, "Login or password is incorrect.");

            var userIdentity = await _uow.UserIdentityRepo.SingleOrDefaultAsync(ui => ui.UserGuid == user.UserGuid);
            var salt = userIdentity.Salt;
            var pepper = _configuration.GetValue<string>("PasswordSettings:Pepper");
            var passwordHash = _passwordHasher.Hash(request.Password + pepper, salt);

            if (user.PasswordHash != passwordHash)
                throw new OperationException(StatusCodes.Status400BadRequest, "Login or password is incorrect.");

            return await _authenticator.Authenticate(user, _uow);
        }

        public async Task<RefreshResult> Refresh(RefreshRequest request)
        {
            if (request == null)
                throw new OperationException(StatusCodes.Status400BadRequest, "Invalid client request.");

            bool isValidRefreshToken = _refreshTokenValidator.Validate(request.RefreshToken);

            if (!isValidRefreshToken)
                throw new OperationException(StatusCodes.Status400BadRequest, "Invalid refresh token.");

            var userRefreshToken = await _uow.UserRefreshTokenRepo.SingleOrDefaultAsync(rt => rt.Token == request.RefreshToken);

            if (userRefreshToken == null)
                throw new OperationException(StatusCodes.Status500InternalServerError, "UserRefreshToken not found.");

            return await _authenticator.RefreshAccessToken(userRefreshToken.User, request.RefreshToken, _uow);
        }

        public async Task<LogoutResult> Logout(int userId)
        {
            var userRefreshToken = await _uow.UserRefreshTokenRepo.FindAsync(userId);

            try
            {
                _uow.UserRefreshTokenRepo.Remove(userRefreshToken);
                await _uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new OperationException(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return new LogoutResult("User has been logged out successfully.");
        }

        public async Task<LoginResult> Authenticate(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload)
        {
            await System.Threading.Tasks.Task.Delay(1);

            var user = await FindUserOrAdd(payload);

            return await _authenticator.Authenticate(user, _uow);
        }

        private async Task<User> FindUserOrAdd(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload)
        {
            var mail = new MailAddress(payload.Email);

            var user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Login == mail.User);
            if (user == null)
            {
                user = new User()
                {
                    Name = payload.Name,
                    LastName = payload.FamilyName,
                    DateOfBirth = DateTime.UtcNow,
                    Login = mail.User,
                    PasswordHash = string.Empty,
                    RoleId = 2 //(int)RoleType.LoggedUser
                };

                await _uow.UserRepo.AddAsync(user);
                await _uow.CompleteAsync();
            }

            return user;
        }
    }
}
