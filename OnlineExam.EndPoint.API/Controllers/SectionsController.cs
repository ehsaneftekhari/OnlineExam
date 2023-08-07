using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.SectionDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.EndPoint.API.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.EndPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        readonly ISectionService _sectionService;

        public SectionsController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var dto = _sectionService.GetById(id);
            return Ok(dto);
        }

        [HttpPost("Create")]
        public IActionResult Create(AddSectionDTO section)
        {
            if (section == null)
                throw new APIValidationException("section can not be null");

            return Ok(_sectionService.Add(section));
        }

        [HttpPost("Update")]
        public IActionResult Update(UpdateSectionDTO section)
        {
            if (section == null)
                throw new APIValidationException("section can not be null");

            _sectionService.Update(section);
            return Ok();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            _sectionService.Delete(id);
                return Ok();
        }
    }
}
