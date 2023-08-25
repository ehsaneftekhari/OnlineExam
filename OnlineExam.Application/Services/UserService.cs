using OnlineExam.Application.Contract.DTOs;
using OnlineExam.Application.Contract.IServices;
using System.Security.Claims;

namespace OnlineExam.Application.Services
{
    public class UserService : IUserService
    {
        readonly IdentityTokenService _tokenService;
        public UserService(IdentityTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public string LogIn(LogInDto logInDto)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Ehsan"),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var t = _tokenService.GenerateToken("www.ehsan.com", "API", claims);
            return t;
        }
    }
}
