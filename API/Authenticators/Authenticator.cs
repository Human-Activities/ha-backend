using API.Models;
using API.Models.Authentication;
using DAL.DataEntities;
using DAL.UnitOfWork;
using Services;
using Services.TokenGenerators;

namespace API.Authenticators
{
    public class Authenticator
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;

        public Authenticator(IConfiguration configuration)
        {
            _accessTokenGenerator = ServicesFactory.CreateAccessTokenGenerator(configuration);
            _refreshTokenGenerator = ServicesFactory.CreateRefreshTokenGenerator(configuration);
        }

        public async Task<LoginResult> Authenticate(User user, IUnitOfWork unitOfWork)
        {
            var userRole = await unitOfWork.UserRoleRepo.SingleOrDefaultAsync(role => role.Id == user.RoleId);
            var accessToken = _accessTokenGenerator.GenerateToken(user, userRole);
            var refreshToken = _refreshTokenGenerator.GenerateToken();

            var userRefreshToken = await unitOfWork.UserRefreshTokenRepo.SingleOrDefaultAsync(urt => urt.UserGuid == user.UserGuid);

            if (userRefreshToken != null)
            {
                userRefreshToken.Token = refreshToken;
                unitOfWork.UserRefreshTokenRepo.Update(userRefreshToken);
            }
            else
            {
                userRefreshToken = new UserRefreshToken
                {
                    Token = refreshToken,
                    UserGuid = user.UserGuid,
                    UserId = user.Id
                };
                await unitOfWork.UserRefreshTokenRepo.AddAsync(userRefreshToken);
            }
            await unitOfWork.CompleteAsync();

            return new LoginResult
            {
                UserGuid = user.UserGuid,
                Successful = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<RefreshResult> RefreshAccessToken(User user, string refreshToken, IUnitOfWork unitOfWork)
        {
            var userRole = await unitOfWork.UserRoleRepo.SingleOrDefaultAsync(role => role.Id == user.RoleId);
            string accessToken = _accessTokenGenerator.GenerateToken(user, userRole);

            return new RefreshResult
            {
                UserGuid = user.UserGuid,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Successful = true
            };
        }
    }
}
