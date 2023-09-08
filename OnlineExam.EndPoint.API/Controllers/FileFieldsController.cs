using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.DTOs.FileFieldDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Services;
using OnlineExam.EndPoint.API.Exceptions;

namespace OnlineExam.EndPoint.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class FileFieldsController : ControllerBase
    {
        readonly IFileFieldService _fileFieldService;

        public FileFieldsController(IFileFieldService fileFieldService)
        {
            _fileFieldService = fileFieldService;
        }

        [HttpGet("[controller]/{id}")]
        public IActionResult GetById(int id)
        {
            var dto = _fileFieldService.GetById(id);
            return Ok(dto);
        }

        [HttpGet("Questions/{id}/[controller]")]
        public IActionResult GetAllByQuestionId(int id, int pageNumber = 1, int pageSize = 20)
        {
            if (pageNumber < 1)
                throw new APIValidationException("pageNumber can not be less than 1");

            if (pageSize < 1)
                throw new APIValidationException("pageSize can not be less than 1");

            var dto = _fileFieldService.GetAllByQuestionId(id, (pageNumber - 1) * pageSize, pageSize);
            return Ok(dto);
        }

        [HttpPost("Questions/{id}/[controller]")]
        public IActionResult Create(int id, AddFileFieldDTO dto)
        {
            if (dto == null)
                throw new APIValidationException("dto can not be null");

            return Ok(_fileFieldService.Add(id, dto));
        }

        [HttpPatch("[controller]/{id}")]
        public IActionResult Update(int id, UpdateFileFieldDTO textField)
        {
            if (textField == null)
                throw new APIValidationException("CheckField can not be null");

            _fileFieldService.Update(id, textField);
            return Ok();
        }

        [HttpDelete("[controller]/{id}")]
        public IActionResult Delete(int id)
        {
            _fileFieldService.Delete(id);
            return Ok();
        }
    }
}
