using Microsoft.IdentityModel.Tokens;
using OnlineExam.Model.ConfigProviders;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineExam.Application.Services
{
    public class IdentityTokenService
    {
        readonly IdentityConfiguration _identityConfiguration;
        public IdentityTokenService(IdentityConfiguration identityConfiguration)
        {
            _identityConfiguration = identityConfiguration;
        }

        public string GenerateToken(string issuer, string audience, IEnumerable<Claim> claims)
        {
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_identityConfiguration.Key));

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
                //EncryptingCredentials = EncryptingCredentials()
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            
            return tokenHandler.WriteToken(tokenHandler.CreateJwtSecurityToken(securityTokenDescriptor));
        }
    }
}
