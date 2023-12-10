using Microsoft.AspNetCore.Identity;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Contract.DTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Model.Constants;
using OnlineExam.Model.Models;
using System.Data;
using System.Security.Claims;

namespace OnlineExam.Application.Services.UserServices
{
    public class UserService : IUserService
    {
        readonly IdentityTokenService _tokenService;
        readonly IUserInternalService _userInternalService;
        readonly SignInManager<IdentityUser> _signInManager;

        public UserService(IdentityTokenService tokenService, IUserInternalService userInternalService, SignInManager<IdentityUser> signInManager)
        {
            _tokenService = tokenService;
            _userInternalService = userInternalService;
            _signInManager = signInManager;
        }

        public string LogIn(LogInDto logInDto)
        {
            if (!_userInternalService.TryGetByName(logInDto.username, out var user, out var exception) && exception is ApplicationSourceNotFoundException)
                throw new ApplicationValidationException("Username or password is incorrect");

            var signInResult = _signInManager.PasswordSignInAsync(user, logInDto.password, false, false).GetAwaiter().GetResult();

            if (!signInResult.Succeeded)
                throw new ApplicationUnAuthorizedException("");

            var claims = new List<Claim>(
                _userInternalService.GetRoles(user).Select(r => new Claim(ClaimTypes.Role, r)))
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            return _tokenService.GenerateToken("www.ehsan.com", claims);
        }

        public bool ValidateToken(string token, IEnumerable<string> expectedRoleNames, out ClaimsPrincipal claimsPrincipal)
        {
            if (!ValidateToken(token, out claimsPrincipal))
                throw new ApplicationUnAuthenticateException("you are not Authenticated");

            if (expectedRoleNames != null && !HasRole(claimsPrincipal, expectedRoleNames))
                throw new ApplicationUnAuthorizedException("you are not Authorized");

            return true;
        }

        public bool ValidateToken(string token, out ClaimsPrincipal claimsPrincipal)
        {
            return _tokenService.ValidateToken("www.ehsan.com", token, out claimsPrincipal);
        }

        public bool HasRole(ClaimsPrincipal claimsPrincipal, IEnumerable<string> roleNames)
        {
            if (roleNames == null)
                throw new ArgumentNullException($"{nameof(roleNames)} can not be null");

            return !roleNames.Any() || roleNames.Any(n => claimsPrincipal.IsInRole(n));
        }

        public string Register(RegisterDto dto)
        {
            var newUser = new IdentityUser(dto.username);
            newUser.PasswordHash = _userInternalService.HashPassword(newUser, dto.password);
            _userInternalService.Add(newUser);
            _userInternalService.AddToRolesAsync(newUser, new string[] { IdentityRoleNames.ExamCreator, IdentityRoleNames.ExamUser });
            return newUser.UserName;
        }
    }
}
