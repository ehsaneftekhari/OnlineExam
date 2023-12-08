using OnlineExam.Application.Contract.DTOs;
using System.Security.Claims;

namespace OnlineExam.Application.Contract.IServices
{
    public interface IUserService
    {
        string LogIn(LogInDto logInDto);
        bool ValidateToken(string token, IEnumerable<string> expectedRoleNames, out ClaimsPrincipal claimsPrincipal);
        bool ValidateToken(string token, out ClaimsPrincipal claimsPrincipal);
        bool HasRole(ClaimsPrincipal claimsPrincipal, IEnumerable<string> roleNames);
        string Register(RegisterDto dto);
    }
}