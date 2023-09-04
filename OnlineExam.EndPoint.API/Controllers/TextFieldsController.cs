using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.EndPoint.API.Exceptions;

namespace OnlineExam.EndPoint.API.Controllers
{
    [ApiController]
    public class TextFieldsController : ControllerBase
    {
        readonly ITextFieldService _textFieldService;

        public TextFieldsController(ITextFieldService textFieldService)
        {
            _textFieldService = textFieldService;
        }

        [HttpGet("[controller]/{id}")]
        public IActionResult GetById(int id)
        {
            var dto = _textFieldService.GetById(id);
            return Ok(dto);
        }

        [HttpGet("Questions/{id}/[controller]")]
        public IActionResult GetAllByQuestionId(int id, int pageNumber = 1, int pageSize = 20)
        {
            if (pageNumber < 1)
                throw new APIValidationException("pageNumber can not be less than 1");

            if (pageSize < 1)
                throw new APIValidationException("pageSize can not be less than 1");

            var dto = _textFieldService.GetAllByExamId(id, (pageNumber - 1) * pageSize, pageSize);
            return Ok(dto);
        }

        [HttpPost("Questions/{id}/[controller]")]
        public IActionResult Create(int id, AddTextFieldDTO textField)
        {
            if (textField == null)
                throw new APIValidationException("Questions can not be null");

            return Ok(_textFieldService.Add(id, textField));
        }

        [HttpPatch("[controller]/{id}")]
        public IActionResult Update(int id, UpdateTextFieldDTO textField)
        {
            if (textField == null)
                throw new APIValidationException("textField can not be null");

            _textFieldService.Update(id, textField);
            return Ok();
        }

        [HttpDelete("[controller]/{id}")]
        public IActionResult Delete(int id)
        {
            _textFieldService.Delete(id);
            return Ok();
        }
    }
}
