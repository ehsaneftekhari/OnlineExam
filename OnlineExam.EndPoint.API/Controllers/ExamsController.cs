﻿using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.ExamDTOs;
using OnlineExam.Application.Contract.IServices;

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

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            if(id < 0)
                return BadRequest("id can not be less than zero");

            var dto = _examService.GetById(id);
            return Ok(dto);
        }

        [HttpPost("GetByFilter")]
        public IActionResult GetByFilter(ExamFilterDTO dto)
        {
            //if (dto < 0)
            //    return BadRequest("id can not be less than zero");

            var pagingModel = _examService.GetByFilter(dto);
            return Ok(pagingModel);
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
                return BadRequest($"did not updated");

            throw new Exception();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest("id can not be less than zero");

            if(_examService.Delete(id))
                return Ok("Deleted");

            return BadRequest($"did not deleted");
        }
    }
}
