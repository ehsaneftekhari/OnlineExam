using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Services;
using OnlineExam.EndPoint.API.Exceptions;

namespace OnlineExam.EndPoint.API.Controllers
{
    [ApiController]
    public class CheckFieldsController : ControllerBase
    {
        readonly ICheckFieldService _checkFieldService;

        public CheckFieldsController(ICheckFieldService checkFieldService)
        {
            _checkFieldService = checkFieldService;
        }

        [HttpGet("[controller]/{id}")]
        public IActionResult GetById(int id)
        {
            var dto = _checkFieldService.GetById(id);
            return Ok(dto);
        }

        [HttpPost("Questions/{id}/[controller]")]
        public IActionResult Create(int id, AddCheckFieldDTO checkField)
        {
            if (checkField == null)
                throw new APIValidationException("CheckField can not be null");

            return Ok(_checkFieldService.Add(id, checkField));
        }
    }
}
