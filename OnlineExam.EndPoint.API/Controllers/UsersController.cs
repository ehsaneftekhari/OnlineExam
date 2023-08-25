using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs;
using OnlineExam.Application.Contract.IServices;

namespace OnlineExam.EndPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult LogIn(LogInDto dto)
        {
            return Ok(_userService.LogIn(dto));
        }
    }
}
