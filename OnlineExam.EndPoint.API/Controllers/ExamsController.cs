﻿using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.ExamDTOs;
using OnlineExam.Application.Contract.IServices;
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

        [HttpPatch]
        public IActionResult Update(UpdateExamDTO exam)
        {
            if (exam == null)
                throw new APIValidationException("exam can not be null");

            _examService.Update(exam);
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
