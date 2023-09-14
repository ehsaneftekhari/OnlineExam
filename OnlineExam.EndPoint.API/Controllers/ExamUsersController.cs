using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.ExamUserDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.EndPoint.API.Exceptions;

namespace OnlineExam.EndPoint.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class ExamUsersController : ControllerBase
    {
        readonly IExamUserService _examUserService;

        public ExamUsersController(IExamUserService examUserService)
        {
            _examUserService = examUserService;
        }

        [HttpGet("Exams/{id}/[controller]")]
        public IActionResult GetAllByExamId(int id, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new APIValidationException("pageNumber can not be less than 1");

            if (pageSize < 1)
                throw new APIValidationException("pageSize can not be less than 1");

            var dto = _examUserService.GetAllByExamId(id, (pageNumber - 1) * pageSize, pageSize);
            return Ok(dto);
        }

        [HttpGet("[controller]/{id}")]
        public IActionResult GetById(int id)
        {
            var dto = _examUserService.GetById(id);
            return Ok(dto);
        }

        [HttpPost("Exams/{id}/[controller]")]
        public IActionResult Create(int id)
        {
            var examUser = new AddExamUserDTO()
            {
                ExamId = id,
                UserId = "1"
            };

            return Ok(_examUserService.Add(examUser));
        }

        [HttpDelete("[controller]/{id}")]
        public IActionResult Delete(int id)
        {
            _examUserService.Delete(id);
            return Ok();
        }
    }
}
