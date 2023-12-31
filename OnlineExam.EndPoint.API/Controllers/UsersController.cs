﻿using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.EndPoint.API.Attributes;
using OnlineExam.Model.Constants;

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

        [HttpPost("LogIn")]
        public IActionResult LogIn(LogInDto dto)
        {
            return Ok(_userService.LogIn(dto));
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterDto dto)
        {
            return Ok(_userService.Register(dto));
        }

        [HttpPost("Test")]
        [AuthorizeActionFilter(IdentityRoleNames.ExamUser, IdentityRoleNames.ExamCreator)]
        public IActionResult Test(string dto)
        {
            return Ok();
        }
    }
}
