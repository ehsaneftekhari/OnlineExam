using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.AnswerDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.EndPoint.API.Attributes;
using OnlineExam.EndPoint.API.DTOs;
using OnlineExam.EndPoint.API.DTOs.AnswerDTOs;
using OnlineExam.EndPoint.API.Exceptions;
using OnlineExam.Model.Constants;

namespace OnlineExam.EndPoint.API.Controllers
{
    [AuthorizeActionFilter(IdentityRoleNames.ExamUser, IdentityRoleNames.ExamCreator)]
    [Route("api/")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        readonly IAnswerService _answerService;
        readonly ScopeDataContainer _scopeDataContainer;

        public AnswersController(IAnswerService answerService, ScopeDataContainer scopeDataContainer)
        {
            _answerService = answerService;
            this._scopeDataContainer = scopeDataContainer;
        }

        [HttpGet("[controller]/{id}")]
        public IActionResult GetById(int id)
        {
            var dto = _answerService.GetById(id, _scopeDataContainer.IdentityUserId);
            return Ok(dto);
        }

        [HttpGet("ExamUsers/{examUserId}/Questions/{questionId}/[controller]")]
        public IActionResult GetAll(int examUserId, int questionId, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new APIValidationException("pageNumber can not be less than 1");

            if (pageSize < 1)
                throw new APIValidationException("pageSize can not be less than 1");

            var dto = _answerService.GetAll(examUserId, questionId, _scopeDataContainer.IdentityUserId, (pageNumber - 1) * pageSize, pageSize);
            return Ok(dto);
        }

        [HttpGet("ExamUsers/{examUserId}/[controller]")]
        public IActionResult GetAllByExamUserId(int examUserId, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new APIValidationException("pageNumber can not be less than 1");

            if (pageSize < 1)
                throw new APIValidationException("pageSize can not be less than 1");

            var dto = _answerService.GetAllByExamUserId(examUserId, _scopeDataContainer.IdentityUserId, (pageNumber - 1) * pageSize, pageSize);
            return Ok(dto);
        }

        [HttpPost("Questions/{questionId}/[controller]")]
        public IActionResult Create(int questionId, AddAnswerApiDTO answer)
        {
            var dto = new AddAnswerDTO() { Content = answer.Content };
            dto.QuestionId = questionId;
            return Ok(_answerService.Add(dto, _scopeDataContainer.IdentityUserId));
        }

        [HttpPatch("[controller]/{id}")]
        public IActionResult Update(int id, UpdateAnswerApiDTO answer)
        {
            var dto = new UpdateAnswerDTO() { EarnedScore = answer.EarnedScore };
            dto.Id = id;
            _answerService.UpdateEarnedScore(dto, _scopeDataContainer.IdentityUserId);
            return Ok();
        }
    }
}
