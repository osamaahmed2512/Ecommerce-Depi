using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Ecommerce.Application.Interfaces
{
    public class TokenService : ItokenService
    {
        private readonly IConfiguration _config;

        public TokenService( IConfiguration config)
        {
            _config = config;
        }
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {

            var TokenHandler = new JwtSecurityTokenHandler();
            var AuthSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["Jwt:Issuer"],
                Audience= _config["Jwt:Audience"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(AuthSignInKey,SecurityAlgorithms.HmacSha256)
            };

            var Token = TokenHandler.CreateToken(TokenDescriptor);
            return TokenHandler.WriteToken(Token);
        }

        public string GenerateRefreshToken()
        {
            var RandomNumber = new byte[32];

            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(RandomNumber);

            return Convert.ToBase64String(RandomNumber);
        }

        public ClaimsPrincipal GetPrincipleFromExpiredToken(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = _config["Jwt:Audience"],
                ValidIssuer = _config["Jwt:Issuer"],
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey
               (Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);


            var jwtSecurityToken = securityToken as JwtSecurityToken;


            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals
            (SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
