using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Services.QuestionServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.CheckFieldServices
{
    public sealed class CheckFieldService : ICheckFieldService
    {
        readonly ICheckFieldInternalService _checkFieldInternalService;
        readonly ICheckFieldMapper _checkFieldMapper;
        readonly ICheckFieldValidator _checkFieldValidator;
        readonly IQuestionInternalService _questionInternalService;

        public CheckFieldService(ICheckFieldInternalService checkFieldInternalService,
                                 ICheckFieldMapper checkFieldMapper,
                                 ICheckFieldValidator checkFieldValidator,
                                 IQuestionInternalService questionInternalService)
        {
            _checkFieldInternalService = checkFieldInternalService;
            _checkFieldMapper = checkFieldMapper;
            _checkFieldValidator = checkFieldValidator;
            _questionInternalService = questionInternalService;
        }

        public ShowCheckFieldDTO Add(int questionId, string issuerUserId,  AddCheckFieldDTO checkField)
        {
            var question = _questionInternalService.GetById(questionId,
                _questionInternalService.GetIQueryable()
                .Include(x => x.Section)
                .ThenInclude(x => x.Exam));

            if(question.Section.Exam.CreatorUserId != issuerUserId)
                throw new ApplicationUnAuthorizedException("User has no access to Question");

            _checkFieldValidator.ValidateDTO(checkField);

            var newCheckField = _checkFieldMapper.AddDTOToEntity(questionId, checkField)!;

            _checkFieldInternalService.Add(newCheckField);

            return _checkFieldMapper.EntityToShowDTO(newCheckField)!;
        }

        public void Delete(int checkFieldId)
            => _checkFieldInternalService.Delete(checkFieldId);

        public IEnumerable<ShowCheckFieldDTO> GetAllByQuestionId(int questionId, int skip = 0, int take = 20)
            => _checkFieldInternalService.GetAllByParentId(questionId, skip, take).Select(_checkFieldMapper.EntityToShowDTO);

        public ShowCheckFieldDTO? GetById(int checkFieldId)
            => _checkFieldMapper.EntityToShowDTO(_checkFieldInternalService.GetById(checkFieldId));

        public void Update(int checkFieldId, UpdateCheckFieldDTO dTO)
        {
            _checkFieldValidator.ValidateDTO(dTO);

            var checkField = _checkFieldInternalService.GetById(checkFieldId);

            _checkFieldMapper.UpdateEntityByDTO(checkField, dTO);

            _checkFieldInternalService.Update(checkField);
        }
    }
}
