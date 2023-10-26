using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OnlineExam.Application.Contract.DTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using System.Data;
using System.Security.Claims;

namespace OnlineExam.Application.Services
{
    public class UserService : IUserService
    {
        readonly IdentityTokenService _tokenService;
        readonly UserManager<IdentityUser> _userManager;
        readonly SignInManager<IdentityUser> _signInManager;

        public UserService(IdentityTokenService tokenService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string LogIn(LogInDto logInDto)
        {
            var user = _userManager.FindByNameAsync(logInDto.username).GetAwaiter().GetResult();

            var signInResult = _signInManager.PasswordSignInAsync(user, logInDto.password, false, false).GetAwaiter().GetResult();

            if (!signInResult.Succeeded)
                throw new ApplicationUnAuthorizedException("");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var t = _tokenService.GenerateToken("www.ehsan.com", claims);
            return t;
        }

        public ValidateTokenResult ValidateToken(string token, IEnumerable<string> expectedRoleNames)
        {
            var result = new ValidateTokenResult() { IsAuthorized = true, IsAuthenticated = true };

            if (!ValidateToken(token, out ClaimsPrincipal claimsPrincipal))
                result.IsAuthenticated = false;

            if (!HasRole(claimsPrincipal, expectedRoleNames))
                    result.IsAuthorized = false;

            return result;
        }

        public bool ValidateToken(string token, out ClaimsPrincipal claimsPrincipal)
        {
            return _tokenService.ValidateToken("www.ehsan.com", token, out claimsPrincipal);
        }

        public bool HasRole(ClaimsPrincipal claimsPrincipal, IEnumerable<string> roleNames)
        {
            return roleNames.Any(n => claimsPrincipal.IsInRole(n));
        }

        public string Register(RegisterDto dto)
        {
            var newUser = new IdentityUser(dto.username);
            newUser.PasswordHash = _userManager.PasswordHasher.HashPassword(newUser, dto.password);
            _userManager.CreateAsync(newUser).GetAwaiter().GetResult();
            return newUser.UserName;
        }
    }
}
