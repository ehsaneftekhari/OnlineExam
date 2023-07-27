using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.SectionDTOs;
using OnlineExam.Application.Contract.IServices;

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
            if (id < 0)
                return BadRequest("id can not be less than zero");

            var dto = _sectionService.GetById(id);
            return Ok(dto);
        }

        [HttpPost("Create")]
        public IActionResult Create(AddSectionDTO section)
        {
            if (section == null)
                return BadRequest();

            if (_sectionService.Add(section))
                return Ok("Created");

            return BadRequest();
        }

        [HttpPost("Update")]
        public IActionResult Update(UpdateSectionDTO section)
        {
            if (section == null)
                return BadRequest();

            if (_sectionService.Update(section))
                return Ok("Updated");

            if (_sectionService.GetById(section.Id) == null)
                return BadRequest($"there is no section by id {section.Id}");

            throw new Exception();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest("id can not be less than zero");

            if (_sectionService.Delete(id))
                return Ok("Deleted");

            return BadRequest($"there is no section by id {id} to deleted");
        }
    }
}
