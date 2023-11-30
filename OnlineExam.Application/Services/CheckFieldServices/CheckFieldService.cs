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

        public ShowCheckFieldDTO Add(int questionId, string issuerUserId, AddCheckFieldDTO checkField)
        {
            var exam = _questionInternalService.GetById(questionId,
                _questionInternalService.GetIQueryable()
                .Include(x => x.Section)
                .ThenInclude(x => x.Exam))
                .Section
                .Exam;

            if (exam.CreatorUserId != issuerUserId)
                throw new ApplicationUnAuthorizedException("User has no access to Question");

            _checkFieldValidator.ValidateDTO(checkField);

            var newCheckField = _checkFieldMapper.AddDTOToEntity(questionId, checkField)!;

            _checkFieldInternalService.Add(newCheckField);

            return _checkFieldMapper.EntityToShowDTO(newCheckField)!;
        }

        public void Delete(int checkFieldId, string issuerUserId)
        {
            var checkField = _checkFieldInternalService.GetById(checkFieldId,
                _checkFieldInternalService.GetIQueryable()
                .Include(x => x.Question)
                .ThenInclude(x => x.Section)
                .ThenInclude(x => x.Exam));

            if (checkField.Question.Section.Exam.CreatorUserId != issuerUserId)
                throw new ApplicationUnAuthorizedException("User has no access to CheckField");

            _checkFieldInternalService.Delete(checkField);
        }

        public IEnumerable<ShowCheckFieldDTO> GetAllByQuestionId(int questionId, string issuerUserId, int skip = 0, int take = 20)
        {
            var exam = _questionInternalService.GetById(questionId,
                _questionInternalService.GetIQueryable()
                .Include(x => x.Section)
                .ThenInclude(x => x.Exam)
                .ThenInclude(x => x.ExamUsers))
                .Section
                .Exam;

            if (exam.CreatorUserId != issuerUserId
                && !exam.ExamUsers.Any(x => x.UserId == issuerUserId))
                throw new ApplicationUnAuthorizedException($"User has no access to Question");

            return _checkFieldInternalService.GetAllByParentId(questionId, skip, take).Select(_checkFieldMapper.EntityToShowDTO);
        }

        public ShowCheckFieldDTO? GetById(int checkFieldId, string issuerUserId)
        {
            var checkField = _checkFieldInternalService.GetById(checkFieldId,
                _checkFieldInternalService.GetIQueryable()
                .Include(x => x.Question)
                .ThenInclude(x => x.Section)
                .ThenInclude(x => x.Exam));

            if (checkField.Question.Section.Exam.CreatorUserId != issuerUserId
                && !checkField.Question.Section.Exam.ExamUsers.Any(x => x.UserId == issuerUserId))
                throw new ApplicationUnAuthorizedException($"User has no access to Question");

            return _checkFieldMapper.EntityToShowDTO(checkField);
        }

        public void Update(int checkFieldId, string issuerUserId, UpdateCheckFieldDTO dTO)
        {
            var checkField = _checkFieldInternalService.GetById(checkFieldId,
                _checkFieldInternalService.GetIQueryable()
                .Include(x => x.Question)
                .ThenInclude(x => x.Section)
                .ThenInclude(x => x.Exam));

            if (checkField.Question.Section.Exam.CreatorUserId != issuerUserId)
                throw new ApplicationUnAuthorizedException("User has no access to CheckField");

            _checkFieldValidator.ValidateDTO(dTO);

            _checkFieldMapper.UpdateEntityByDTO(checkField, dTO);

            _checkFieldInternalService.Update(checkField);
        }
    }
}
