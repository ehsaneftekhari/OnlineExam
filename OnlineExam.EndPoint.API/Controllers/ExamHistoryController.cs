using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Services;

namespace OnlineExam.EndPoint.API.Controllers
{
    [Route("api/Exam/History")]
    [ApiController]
    public class ExamHistoryController : ControllerBase
    {
        IExamHistoryService _service;

        public ExamHistoryController(IExamHistoryService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id < 0)
                return BadRequest("id can not be less than zero");

            var dto = _service.GetById(id);
            return Ok(dto);
        }
    }
}
