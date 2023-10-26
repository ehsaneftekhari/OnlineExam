using OnlineExam.Application.Contract.DTOs;
using System.Security.Claims;

namespace OnlineExam.Application.Contract.IServices
{
    public interface IUserService
    {
        string LogIn(LogInDto logInDto);
        ValidateTokenResult ValidateToken(string token, IEnumerable<string> expectedRoleNames);
        bool ValidateToken(string token, out ClaimsPrincipal claimsPrincipal);
        bool HasRole(ClaimsPrincipal claimsPrincipal, IEnumerable<string> roleNames);
        string Register(RegisterDto dto);
    }
}