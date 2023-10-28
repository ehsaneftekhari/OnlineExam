using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.QuestionDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.EndPoint.API.Attributes;
using OnlineExam.EndPoint.API.Exceptions;
using OnlineExam.Model.Constants;

namespace OnlineExam.EndPoint.API.Controllers
{
    [AuthorizeActionFilter(IdentityRoleNames.ExamUser, IdentityRoleNames.ExamCreator)]
    [Route("api")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet("[controller]/{id}")]
        public IActionResult GetById(int id)
        {
            var dto = _questionService.GetById(id);
            return Ok(dto);
        }

        [HttpGet("Sections/{id}/[controller]")]
        public IActionResult GetAllBySectionId(int id, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new APIValidationException("pageNumber can not be less than 1");

            if (pageSize < 1)
                throw new APIValidationException("pageSize can not be less than 1");

            var dto = _questionService.GetAllBySectionId(id, (pageNumber - 1) * pageSize, pageSize);
            return Ok(dto);
        }

        [HttpPost("Sections/{id}/[controller]")]
        public IActionResult Create(int id, AddQuestionDTO question)
        {
            if (question == null)
                throw new APIValidationException("question can not be null");

            return Ok(_questionService.Add(id, question));
        }

        [HttpPatch("[controller]/{id}")]
        public IActionResult Update(int id, UpdateQuestionDTO question)
        {
            if (question == null)
                throw new APIValidationException("question can not be null");

            _questionService.Update(id, question);
            return Ok();
        }

        [HttpDelete("[controller]/{id}")]
        public IActionResult Delete(int id)
        {
            _questionService.Delete(id);
            return Ok();
        }
    }
}
