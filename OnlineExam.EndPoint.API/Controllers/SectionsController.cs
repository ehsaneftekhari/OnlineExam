using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.SectionDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.EndPoint.API.Exceptions;

namespace OnlineExam.EndPoint.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        readonly ISectionService _sectionService;

        public SectionsController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        [HttpGet("Exams/{id}/[controller]")]
        public IActionResult GetAllByExamId(int id, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new APIValidationException("pageNumber can not be less than 1");

            if (pageSize < 1)
                throw new APIValidationException("pageSize can not be less than 1");

            var dto = _sectionService.GetAllByExamId(id, (pageNumber - 1) * pageSize, pageSize);
            return Ok(dto);
        }

        [HttpGet("[controller]/{id}")]
        public IActionResult GetById(int id)
        {
            var dto = _sectionService.GetById(id);
            return Ok(dto);
        }

        [HttpPost("Exams/{id}/[controller]")]
        public IActionResult Create(int id, AddSectionDTO section)
        {
            if (section == null)
                throw new APIValidationException("section can not be null");

            return Ok(_sectionService.Add(id, section));
        }

        [HttpPatch("[controller]/{id}")]
        public IActionResult Update(int id, UpdateSectionDTO section)
        {
            if (section == null)
                throw new APIValidationException("section can not be null");

            _sectionService.Update(id, section);
            return Ok();
        }

        [HttpDelete("[controller]/{id}")]
        public IActionResult Delete(int id)
        {
            _sectionService.Delete(id);
            return Ok();
        }
    }
}
