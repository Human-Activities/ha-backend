using API.Authenticators;
using API.Models;
using Castle.Core.Smtp;
using DAL.DataEntities;
using DAL.UnitOfWork;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.TokenValidators;
using Services;
using System.Data;
using Services.PasswordHasher;
using API.Models.TransferObjects;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly Authenticator _authenticator;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly AccessTokenValidator _accessTokenValidator;

        public AuthenticationController(IConfiguration configuration, Authenticator authenticator)
        {
            _configuration = configuration;
            _uow = DataAccessLayerFactory.CreateUnitOfWork();
            _passwordHasher = ServicesFactory.CreateBCryptPasswordHasher();
            _authenticator = authenticator;
            _refreshTokenValidator = ServicesFactory.CreateRefreshTokenValidator(_configuration);
            _accessTokenValidator = ServicesFactory.CreateAccessTokenValidator(_configuration);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RequestResult>> Register([FromBody] RegisterModel registerModel)
        {
            User? user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Login == registerModel.Login);
            if (user != null)
            {
                return BadRequest(new RequestResult { Successful = false, Message = "Uzytkownik o takim loginie juz istnieje" });
            }

            string salt = _passwordHasher.GenerateSalt();

            UserIdentity userIdentity = await _uow.UserIdentityRepo.SingleOrDefaultAsync(ui => ui.Salt == salt);
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

            string pepper = _configuration.GetValue<string>("PasswordSettings:Pepper");
            string passwordHash = _passwordHasher.Hash(registerModel.Password + pepper, salt);

            User newUser = new User()
            {
                Name = registerModel.Name,
                LastName = registerModel.LastName,
                DateOfBirth = registerModel.DateOfBirth,
                Login = registerModel.Login,
                PasswordHash = passwordHash,
                RoleId = 2
            };

            await _uow.UserRepo.AddAsync(newUser);
            await _uow.CompleteAsync();

            User createdUser = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Login == registerModel.Login);

            UserIdentity createdUserIdentity = new UserIdentity()
            {
                UserId = createdUser.Id,
                Salt = salt
            };

            await _uow.UserIdentityRepo.AddAsync(createdUserIdentity);
            await _uow.CompleteAsync();

            return Ok(new RequestResult { Successful = true, Message = "Poprawnie utworzono użytkownika." });
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("Invalid client request");
            }

            User? user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Login == loginModel.Login);

            if (user != null)
            {
                UserIdentity userIdentity = await _uow.UserIdentityRepo.SingleOrDefaultAsync(ui => ui.UserId == user.Id);
                string salt = userIdentity.Salt;
                string pepper = _configuration.GetValue<string>("PasswordSettings:Pepper");
                string passwordHash = _passwordHasher.Hash(loginModel.Password + pepper, salt);

                if (user.PasswordHash == passwordHash)
                {
                    RequestResult response = await _authenticator.Authenticate(user, _uow);
                    return Ok(response);
                }
            }

            return BadRequest(new RequestResult { Successful = false, Message = "Nazwa użytownika lub hasło jest niepoprawne." });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            if (refreshRequest == null)
            {
                return BadRequest("Invalid client request");
            }

            bool isValidAccessToken = _accessTokenValidator.Validate(refreshRequest.AccessToken);
            if (!isValidAccessToken)
            {
                return BadRequest("Invalid access token");
            }

            string userId = HttpContext.User.FindFirstValue("id");

            UserRefreshToken? userRefreshToken = await _uow.UserRefreshTokenRepo.SingleOrDefaultAsync(rt => rt.UserId == Guid.Parse(userId));
            if (userRefreshToken == null)
            {
                return NotFound("Invalid refresh token");
            }
            bool isValidRefreshToken = _refreshTokenValidator.Validate(userRefreshToken.Token);

            if (!isValidRefreshToken)
            {
                return BadRequest("Invalid access token");
            }

            User? user = await _uow.UserRepo.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userId));
            if (user == null)
            {
                return NotFound("User not found");
            }

            RequestResult response = await _authenticator.RefreshAccessToken(user, userRefreshToken.Token, _uow);
            return Ok(response);
        }

        [HttpDelete("logout")]
        public async Task<IActionResult> Logout()
        {
            string rawUserId = HttpContext.User.FindFirstValue("id");
            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }
            UserRefreshToken userRefreshToken = await _uow.UserRefreshTokenRepo.SingleOrDefaultAsync(urt => urt.UserId == userId);
            _uow.UserRefreshTokenRepo.Remove(userRefreshToken);
            await _uow.CompleteAsync();

            return Ok();
        }
    }
}
