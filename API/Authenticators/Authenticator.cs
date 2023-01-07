using API.Models;
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

        public async Task<RequestResult> Authenticate(User user, IUnitOfWork unitOfWork)
        {
            UserRole userRole = await unitOfWork.UserRoleRepo.SingleOrDefaultAsync(role => role.Id == user.RoleId);
            string accessToken = _accessTokenGenerator.GenerateToken(user, userRole);
            string refreshToken = _refreshTokenGenerator.GenerateToken();

            UserRefreshToken userRefreshToken = await unitOfWork.UserRefreshTokenRepo.SingleOrDefaultAsync(urt => urt.UserId == user.Id);

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
                    UserId = user.Id
                };
                await unitOfWork.UserRefreshTokenRepo.AddAsync(userRefreshToken);
            }
            await unitOfWork.CompleteAsync();

            return new RequestResult
            {
                Successful = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<RequestResult> RefreshAccessToken(User user, string refreshToken, IUnitOfWork unitOfWork)
        {
            UserRole userRole = await unitOfWork.UserRoleRepo.SingleOrDefaultAsync(role => role.Id == user.RoleId);
            string accessToken = _accessTokenGenerator.GenerateToken(user, userRole);

            return new RequestResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
