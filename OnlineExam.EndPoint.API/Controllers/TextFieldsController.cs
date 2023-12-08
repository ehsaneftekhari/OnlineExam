using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.EndPoint.API.Attributes;
using OnlineExam.EndPoint.API.DTOs;
using OnlineExam.EndPoint.API.Exceptions;
using OnlineExam.Model.Constants;

namespace OnlineExam.EndPoint.API.Controllers
{
    [AuthorizeActionFilter(IdentityRoleNames.ExamUser, IdentityRoleNames.ExamCreator)]
    [Route("api")]
    [ApiController]
    public class TextFieldsController : ControllerBase
    {
        readonly ITextFieldService _textFieldService;
        readonly ScopeDataContainer _scopeDataContainer;

        public TextFieldsController(ITextFieldService textFieldService,
                                    ScopeDataContainer scopeDataContainer)
        {
            _textFieldService = textFieldService;
            _scopeDataContainer = scopeDataContainer;
        }

        [HttpGet("[controller]/{id}")]
        public IActionResult GetById(int id)
        {
            var dto = _textFieldService.GetById(id, _scopeDataContainer.IdentityUserId);
            return Ok(dto);
        }

        [HttpGet("Questions/{id}/[controller]")]
        public IActionResult GetAllByQuestionId(int id, int pageNumber = 1, int pageSize = 20)
        {
            if (pageNumber < 1)
                throw new APIValidationException("pageNumber can not be less than 1");

            if (pageSize < 1)
                throw new APIValidationException("pageSize can not be less than 1");

            var dto = _textFieldService.GetAllByQuestionId(id, _scopeDataContainer.IdentityUserId, (pageNumber - 1) * pageSize, pageSize);
            return Ok(dto);
        }

        [HttpPost("Questions/{id}/[controller]")]
        public IActionResult Create(int id, AddTextFieldDTO textField)
        {
            if (textField == null)
                throw new APIValidationException("TextField can not be null");

            return Ok(_textFieldService.Add(id, _scopeDataContainer.IdentityUserId, textField));
        }

        [HttpPatch("[controller]/{id}")]
        public IActionResult Update(int id, UpdateTextFieldDTO textField)
        {
            if (textField == null)
                throw new APIValidationException("textField can not be null");

            _textFieldService.Update(id, _scopeDataContainer.IdentityUserId, textField);
            return Ok();
        }

        [HttpDelete("[controller]/{id}")]
        public IActionResult Delete(int id)
        {
            _textFieldService.Delete(id, _scopeDataContainer.IdentityUserId);
            return Ok();
        }
    }
}
