using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.EndPoint.API.Exceptions;

namespace OnlineExam.EndPoint.API.Controllers
{
    [Route("api")]
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

        [HttpGet("Questions/{id}/[controller]")]
        public IActionResult GetAllByQuestionId(int id, int pageNumber = 1, int pageSize = 20)
        {
            if (pageNumber < 1)
                throw new APIValidationException("pageNumber can not be less than 1");

            if (pageSize < 1)
                throw new APIValidationException("pageSize can not be less than 1");

            var dto = _checkFieldService.GetAllByQuestionId(id, (pageNumber - 1) * pageSize, pageSize);
            return Ok(dto);
        }

        [HttpPost("Questions/{id}/[controller]")]
        public IActionResult Create(int id, AddCheckFieldDTO checkField)
        {
            if (checkField == null)
                throw new APIValidationException("CheckField can not be null");

            return Ok(_checkFieldService.Add(id, checkField));
        }

        [HttpPatch("[controller]/{id}")]
        public IActionResult Update(int id, UpdateCheckFieldDTO textField)
        {
            if (textField == null)
                throw new APIValidationException("CheckField can not be null");

            _checkFieldService.Update(id, textField);
            return Ok();
        }


        [HttpDelete("[controller]/{id}")]
        public IActionResult Delete(int id)
        {
            _checkFieldService.Delete(id);
            return Ok();
        }
    }
}
