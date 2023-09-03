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

        [HttpPost("Questions/{id}/[controller]")]
        public IActionResult Create(int id, AddTextFieldDTO textField)
        {
            if (textField == null)
                throw new APIValidationException("Questions can not be null");

            return Ok(_textFieldService.Add(id, textField));
        }
    }
}
