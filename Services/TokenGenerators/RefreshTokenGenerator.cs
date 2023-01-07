using Microsoft.Extensions.Configuration;

namespace Services.TokenGenerators
{
    public class RefreshTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public RefreshTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken()
        {
            return TokenGenerator.GenerateToken(_configuration.GetSection("JWTSettings").GetSection("RefreshSecretKey").Value,
                _configuration.GetSection("JWTSettings").GetSection("Issuer").Value,
                _configuration.GetSection("JWTSettings").GetSection("Audience").Value,
                double.Parse(_configuration.GetSection("JWTSettings").GetSection("RefreshTokenExpirationTime").Value));
        }
    }
}
