using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.ExamDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Services;
using OnlineExam.EndPoint.API.Exceptions;

namespace OnlineExam.EndPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        readonly IExamService _examService;

        public ExamsController(IExamService examService)
        {
            _examService = examService;
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
        public IActionResult Create(AddExamDTO exam)
        {
            if (exam == null)
                throw new APIValidationException("exam can not be null");

            return Ok(_examService.Add(exam));
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
