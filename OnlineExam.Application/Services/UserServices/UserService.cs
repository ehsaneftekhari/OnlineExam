using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OnlineExam.Application.Contract.DTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Infrastructure.SeedData;
using System.Data;
using System.Security.Claims;

namespace OnlineExam.Application.Services.UserServices
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

            var claims = new List<Claim>(
                _userManager.GetRolesAsync(user).GetAwaiter().GetResult().Select(r => new Claim(ClaimTypes.Role, r)))
            {
                new Claim(ClaimTypes.Name, user.UserName) 
            };

            return _tokenService.GenerateToken("www.ehsan.com", claims);
        }

        public bool ValidateToken(string token, IEnumerable<string> expectedRoleNames)
        {
            if (!ValidateToken(token, out ClaimsPrincipal claimsPrincipal))
                throw new ApplicationUnAuthenticateException("you are not Authenticated");

            if (!HasRole(claimsPrincipal, expectedRoleNames))
                throw new ApplicationUnAuthorizedException("you are not Authorized");

            return true;
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
            _userManager.AddToRolesAsync(newUser, new string[] { IdentityRoleNames.ExamCreator, IdentityRoleNames.ExamUser }).GetAwaiter().GetResult(); ;
            return newUser.UserName;
        }
    }
}
