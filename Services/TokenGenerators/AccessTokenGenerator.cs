using DAL.DataEntities;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Services.TokenGenerators
{
    public class AccessTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public AccessTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(User user, UserRole userRole)
        {
            var claims = new List<Claim>()
                    {
                        new Claim("id", user.Id.ToString()),
                        new Claim(ClaimTypes.Role, userRole.Name)
                    };

            return TokenGenerator.GenerateToken(_configuration.GetSection("JWTSettings").GetSection("AccessSecretKey").Value,
                _configuration.GetSection("JWTSettings").GetSection("Issuer").Value,
                _configuration.GetSection("JWTSettings").GetSection("Audience").Value,
                double.Parse(_configuration.GetSection("JWTSettings").GetSection("AccessTokenExpirationTime").Value),
                claims);
        }
    }
}
