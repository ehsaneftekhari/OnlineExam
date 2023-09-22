using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.AnswerDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.EndPoint.API.DTOs.AnswerDTOs;
using OnlineExam.EndPoint.API.Exceptions;

namespace OnlineExam.EndPoint.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        readonly IAnswerService _answerService;

        public AnswersController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet("[controller]/{id}")]
        public IActionResult GetById(int id)
        {
            var dto = _answerService.GetById(id);
            return Ok(dto);
        }

        [HttpGet("ExamUsers/{examUserId}/Questions/{questionId}/[controller]")]
        public IActionResult GetAll(int examUserId, int questionId, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new APIValidationException("pageNumber can not be less than 1");

            if (pageSize < 1)
                throw new APIValidationException("pageSize can not be less than 1");

            var dto = _answerService.GetAll(examUserId, questionId, (pageNumber - 1) * pageSize, pageSize);
            return Ok(dto);
        }

        [HttpGet("ExamUsers/{examUserId}/[controller]")]
        public IActionResult GetAllByExamUserId(int examUserId, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new APIValidationException("pageNumber can not be less than 1");

            if (pageSize < 1)
                throw new APIValidationException("pageSize can not be less than 1");

            var dto = _answerService.GetAllByExamUserId(examUserId, (pageNumber - 1) * pageSize, pageSize);
            return Ok(dto);
        }

        [HttpPost("ExamUsers/{examUserId}/Questions/{questionId}/[controller]")]
        public IActionResult Create(int examUserId, int questionId, AddAnswerApiDTO answer)
        {
            var dto = new AddAnswerDTO() { Content = answer.Content };
            dto.QuestionId = questionId;
            dto.ExamUserId = examUserId;
            return Ok(_answerService.Add(dto));
        }

        [HttpPatch("[controller]/{id}")]
        public IActionResult Update(int id, UpdateAnswerApiDTO answer)
        {
            var dto = new UpdateAnswerDTO() { EarnedScore = answer.EarnedScore };
            dto.Id = id;
            _answerService.UpdateEarnedScore(dto);
            return Ok();
        }
    }
}
