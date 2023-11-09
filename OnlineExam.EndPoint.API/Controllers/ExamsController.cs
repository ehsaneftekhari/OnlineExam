using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.ExamDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.EndPoint.API.Attributes;
using OnlineExam.EndPoint.API.DTOs;
using OnlineExam.EndPoint.API.DTOs.ExamDTOs;
using OnlineExam.EndPoint.API.Exceptions;
using OnlineExam.Model.Constants;

namespace OnlineExam.EndPoint.API.Controllers
{
    [AuthorizeActionFilter(IdentityRoleNames.ExamUser, IdentityRoleNames.ExamCreator)]
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        readonly IExamService _examService;
        readonly ScopeDataContainer _scopeDataContainer;

        public ExamsController(IExamService examService, ScopeDataContainer scopeDataContainer)
        {
            _examService = examService;
            _scopeDataContainer = scopeDataContainer;
        }

        [HttpGet]
        public IActionResult GetAll(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new APIValidationException("pageNumber can not be less than 1");

            if (pageSize < 1)
                throw new APIValidationException("pageSize can not be less than 1");

            var dto = _examService.GetAll((pageNumber - 1) * pageSize, pageSize);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var dto = _examService.GetById(id);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create(AddExamApiDTO exam)
        {
            if (exam == null)
                throw new APIValidationException("exam can not be null");

            var newExam = new AddExamDTO()
            {
                CreatorUserId = _scopeDataContainer.IdentityUserId,
                Title = exam.Title,
                Start = exam.Start,
                End = exam.End,
                Published = exam.Published
            };

            return Ok(_examService.Add(newExam));
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, UpdateExamDTO exam)
        {
            if (exam == null)
                throw new APIValidationException("exam can not be null");

            _examService.Update(id, exam);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _examService.Delete(id);
            return Ok();
        }
    }
}
