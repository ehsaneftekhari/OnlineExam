using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.SectionDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.EndPoint.API.Attributes;
using OnlineExam.EndPoint.API.DTOs;
using OnlineExam.EndPoint.API.DTOs.SectionDTOs;
using OnlineExam.EndPoint.API.Exceptions;
using OnlineExam.Model.Constants;
using OnlineExam.Model.Models;

namespace OnlineExam.EndPoint.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        readonly ISectionService _sectionService;
        readonly ScopeDataContainer _scopeDataContainer;

        public SectionsController(ISectionService sectionService, ScopeDataContainer scopeDataContainer)
        {
            _sectionService = sectionService;
            _scopeDataContainer = scopeDataContainer;
        }

        [AuthorizeActionFilter(IdentityRoleNames.ExamUser, IdentityRoleNames.ExamCreator)]
        [HttpGet("Exams/{id}/[controller]")]
        public IActionResult GetAllByExamId(int id, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new APIValidationException("pageNumber can not be less than 1");

            if (pageSize < 1)
                throw new APIValidationException("pageSize can not be less than 1");

            var dto = _sectionService.GetAllByExamId(id, _scopeDataContainer.IdentityUserId, (pageNumber - 1) * pageSize, pageSize);
            return Ok(dto);
        }

        [AuthorizeActionFilter(IdentityRoleNames.ExamUser, IdentityRoleNames.ExamCreator)]
        [HttpGet("[controller]/{id}")]
        public IActionResult GetById(int id)
        {
            var dto = _sectionService.GetById(id, _scopeDataContainer.IdentityUserId);
            return Ok(dto);
        }

        [AuthorizeActionFilter(IdentityRoleNames.ExamCreator)]
        [HttpPost("Exams/{id}/[controller]")]
        public IActionResult Create(int id, AddSectionApiDTO section)
        {
            if (section == null)
                throw new APIValidationException("section can not be null");

            return Ok(_sectionService.Add(id, new()
            {
                Title = section.Title,
                Order = section.Order,
                RandomizeQuestions = section.RandomizeQuestions,
                CreatorUserId = _scopeDataContainer.IdentityUserId
            }));
        }

        [AuthorizeActionFilter(IdentityRoleNames.ExamCreator)]
        [HttpPatch("[controller]/{id}")]
        public IActionResult Update(int id, UpdateSectionDTO section)
        {
            if (section == null)
                throw new APIValidationException("section can not be null");

            _sectionService.Update(id, _scopeDataContainer.IdentityUserId, section);
            return Ok();
        }

        [AuthorizeActionFilter(IdentityRoleNames.ExamCreator)]
        [HttpDelete("[controller]/{id}")]
        public IActionResult Delete(int id)
        {
            _sectionService.Delete(id, _scopeDataContainer.IdentityUserId);
            return Ok();
        }
    }
}
