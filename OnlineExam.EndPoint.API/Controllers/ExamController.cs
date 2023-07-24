using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs;
using OnlineExam.Application.Contract.IServices;

namespace OnlineExam.EndPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            if(id < 0)
                return BadRequest("id can not be less than zero");

            var dto = _examService.GetById(id);
            return Ok(dto);
        }

        [HttpPost("Create")]
        public IActionResult Create(AddExamDTO exam)
        {
            if (exam == null)
                return BadRequest();

            if (_examService.Add(exam))
                return Ok("Created");

            return BadRequest();
        }

        [HttpPost("Update")]
        public IActionResult Update(UpdateExamDTO exam)
        {
            if(exam == null)
                return BadRequest();

            if (_examService.Update(exam))
                return Ok("Updated");
            
            if(_examService.GetById(exam.Id) == null)
                return BadRequest($"there is no exam by id {exam.Id}");

            throw new Exception();
            return BadRequest("Did not updated");
        }
    }
}
