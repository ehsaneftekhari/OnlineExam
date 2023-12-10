using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.AllowedFileTypesFieldDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.EndPoint.API.Attributes;
using OnlineExam.EndPoint.API.Exceptions;
using OnlineExam.Model.Constants;

namespace OnlineExam.EndPoint.API.Controllers
{
    [AuthorizeActionFilter(IdentityRoleNames.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class AllowedFileTypesFieldsController : ControllerBase
    {
        readonly IAllowedFileTypesFieldService _allowedFileTypesFieldService;

        public AllowedFileTypesFieldsController(IAllowedFileTypesFieldService allowedFileTypesFieldService)
        {
            _allowedFileTypesFieldService = allowedFileTypesFieldService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var dto = _allowedFileTypesFieldService.GetById(id);
            return Ok(dto);
        }

        [HttpGet]
        public IActionResult GetAll(int pageNumber = 1, int pageSize = 20)
        {
            if (pageNumber < 1)
                throw new APIValidationException("pageNumber can not be less than 1");

            if (pageSize < 1)
                throw new APIValidationException("pageSize can not be less than 1");

            var dto = _allowedFileTypesFieldService.GetAll((pageNumber - 1) * pageSize, pageSize);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create(AddAllowedFileTypesFieldDTO dto)
        {
            if (dto == null)
                throw new APIValidationException("dto can not be null");

            return Ok(_allowedFileTypesFieldService.Add(dto));
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, UpdateAllowedFileTypesFieldDTO dto)
        {
            if (dto == null)
                throw new APIValidationException("dto can not be null");

            _allowedFileTypesFieldService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _allowedFileTypesFieldService.Delete(id);
            return Ok();
        }
    }
}
