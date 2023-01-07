using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Services.TokenValidators
{
    public class AccessTokenValidator
    {
        private readonly IConfiguration _configuration;

        public AccessTokenValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Validate(string accessToken)
        {
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = _configuration.GetSection("JWTSettings").GetSection("Issuer").Value,
                ValidAudience = _configuration.GetSection("JWTSettings").GetSection("Audience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("JWTSettings").GetSection("AccessSecretKey").Value)),
                ClockSkew = TimeSpan.Zero
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(accessToken, validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
