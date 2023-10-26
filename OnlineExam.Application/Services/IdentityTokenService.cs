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
        JwtSecurityTokenHandler _tokenHandler;
        SecurityKey _signingSecurityKey;
        SecurityKey _encryptingSecurityKey;
        public IdentityTokenService(IdentityConfiguration identityConfiguration)
        {
            _identityConfiguration = identityConfiguration;
            _tokenHandler = new JwtSecurityTokenHandler();
            _signingSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_identityConfiguration.TokenSigningKey));
            _encryptingSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_identityConfiguration.TokenEncryptingKey));
        }

        public string GenerateToken(string issuer, IEnumerable<Claim> claims)
        {
            var now = DateTime.UtcNow;
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = issuer,
                IssuedAt = now,
                NotBefore = now,
                Expires = now + _identityConfiguration.ExpirationMinutes,
                Audience = _identityConfiguration.Audience,
                SigningCredentials = new SigningCredentials(_signingSecurityKey, SecurityAlgorithms.HmacSha256Signature),
                EncryptingCredentials = new EncryptingCredentials(_encryptingSecurityKey, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256)
            };

            return _tokenHandler.WriteToken(_tokenHandler.CreateJwtSecurityToken(securityTokenDescriptor));
        }

        public bool ValidateToken(string issuer, string token, out ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal = null;
            try
            {
                claimsPrincipal = _tokenHandler.ValidateToken(token, new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    
                    RequireAudience = true,
                    ValidateAudience = true,
                    ValidAudience = _identityConfiguration.Audience,

                    ValidateIssuer = true,
                    ValidIssuer = issuer,

                    ValidateLifetime = true,
                    LifetimeValidator = LifetimeValidator,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _signingSecurityKey,

                    TokenDecryptionKey = _encryptingSecurityKey
                }, out _);

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private bool LifetimeValidator(DateTime? notBefore,DateTime? expires,
            SecurityToken securityToken,TokenValidationParameters validationParameters)
            => notBefore + _identityConfiguration.ExpirationMinutes == expires;
    }
}
