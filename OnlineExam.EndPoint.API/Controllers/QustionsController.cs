using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.QuestionDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Services;
using OnlineExam.EndPoint.API.Exceptions;

namespace OnlineExam.EndPoint.API.Controllers
{
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

        [HttpGet("Sections/{sectionId}/[controller]")]
        public IActionResult GetAllBySectionId(int sectionId, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new APIValidationException("pageNumber can not be less than 1");

            if (pageSize < 1)
                throw new APIValidationException("pageSize can not be less than 1");

            var dto = _questionService.GetAllBySectionId(sectionId, (pageNumber - 1) * pageSize, pageSize);
            return Ok(dto);
        }

        [HttpPost("[controller]")]
        public IActionResult Create(AddQuestionDTO question)
        {
            if (question == null)
                throw new APIValidationException("question can not be null");

            return Ok(_questionService.Add(question));
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
