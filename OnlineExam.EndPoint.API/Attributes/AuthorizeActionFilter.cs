using Microsoft.AspNetCore.Mvc.Filters;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.EndPoint.API.DTOs;
using System.Security.Claims;

namespace OnlineExam.EndPoint.API.Attributes
{
    public class AuthorizeActionFilter : ActionFilterAttribute
    {
        IUserService _userService;

        public AuthorizeActionFilter(params string[] roles)
        {
            Roles = roles;
        }

        public IEnumerable<string> Roles { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
            var authorizationHeader = context.HttpContext.Request.Headers.Authorization.ToString();

            if (string.IsNullOrWhiteSpace(authorizationHeader) || !authorizationHeader.Contains("Bearer "))
                throw new ApplicationUnAuthenticateException("you are not Authenticated");

            _userService.ValidateToken(authorizationHeader.Replace("Bearer ", ""), Roles, out var claimsPrincipal);

            context.HttpContext.User = claimsPrincipal;

            var scopeDataContainer = context.HttpContext.RequestServices.GetRequiredService<ScopeDataContainer>();
            scopeDataContainer.IdentityUserId = claimsPrincipal.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;

            base.OnActionExecuting(context);
        }
    }
}
