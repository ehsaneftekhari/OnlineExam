﻿using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Services;
using OnlineExam.EndPoint.API.Exceptions;

namespace OnlineExam.EndPoint.API.Controllers
{
    [ApiController]
    public class CheckFieldOptionsController : ControllerBase
    {
        readonly ICheckFieldOptionService _CheckOptionService;

        public CheckFieldOptionsController(ICheckFieldOptionService checkOptionService)
        {
            _CheckOptionService = checkOptionService;
        }

        [HttpGet("[controller]/{id}")]
        public IActionResult GetById(int id)
        {
            var dto = _CheckOptionService.GetById(id);
            return Ok(dto);
        }

        [HttpGet("CheckFields/{id}/[controller]")]
        public IActionResult GetAllByQuestionId(int id, int pageNumber = 1, int pageSize = 20)
        {
            if (pageNumber < 1)
                throw new APIValidationException("pageNumber can not be less than 1");

            if (pageSize < 1)
                throw new APIValidationException("pageSize can not be less than 1");

            var dto = _CheckOptionService.GetAllByCheckFieldId(id, (pageNumber - 1) * pageSize, pageSize);
            return Ok(dto);
        }

        [HttpPost("CheckFields/{id}/[controller]")]
        public IActionResult Create(int id, AddCheckFieldOptionDTO checkFieldOption)
        {
            if (checkFieldOption == null)
                throw new APIValidationException("checkFieldOption can not be null");

            return Ok(_CheckOptionService.Add(id, checkFieldOption));
        }

        [HttpPatch("[controller]/{id}")]
        public IActionResult Update(int id, UpdateCheckFieldOptionDTO textField)
        {
            if (textField == null)
                throw new APIValidationException("checkFieldOption can not be null");

            _CheckOptionService.Update(id, textField);
            return Ok();
        }

        [HttpDelete("[controller]/{id}")]
        public IActionResult Delete(int id)
        {
            _CheckOptionService.Delete(id);
            return Ok();
        }
    }
}
