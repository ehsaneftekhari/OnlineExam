using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.CheckFieldServices
{
    public sealed class CheckFieldService : ICheckFieldService
    {
        readonly ICheckFieldInternalService _checkFieldInternalService;
        readonly ICheckFieldMapper _checkFieldMapper;
        readonly ICheckFieldValidator _checkFieldValidator;

        public CheckFieldService(ICheckFieldInternalService checkFieldInternalService,
                                 ICheckFieldMapper checkFieldMapper,
                                 ICheckFieldValidator checkFieldValidator)
        {
            _checkFieldInternalService = checkFieldInternalService;
            _checkFieldMapper = checkFieldMapper;
            _checkFieldValidator = checkFieldValidator;
        }

        public ShowCheckFieldDTO Add(int questionId, string issuerUserId, AddCheckFieldDTO checkField)
        {
            _checkFieldValidator.ThrowIfUserIsNotExamCreator(questionId, issuerUserId);

            _checkFieldValidator.ValidateDTO(checkField);

            var newCheckField = _checkFieldMapper.AddDTOToEntity(questionId, checkField)!;

            _checkFieldInternalService.Add(newCheckField);

            return _checkFieldMapper.EntityToShowDTO(newCheckField)!;
        }

        public void Delete(int checkFieldId, string issuerUserId)
        {
            var checkField = GetCheckFieldWith_Question_Section_Exam_Included(checkFieldId);

            if (checkField.Question.Section.Exam.CreatorUserId != issuerUserId)
                throw new ApplicationUnAuthorizedException("User has no access to CheckField");

            _checkFieldInternalService.Delete(checkField);
        }

        public IEnumerable<ShowCheckFieldDTO> GetAllByQuestionId(int questionId, string issuerUserId, int skip = 0, int take = 20)
        {
            _checkFieldValidator.ThrowIfUserIsNotExamCreatorOrExamUser(questionId, issuerUserId);

            return _checkFieldInternalService.GetAllByParentId(questionId, skip, take).Select(_checkFieldMapper.EntityToShowDTO);
        }

        public ShowCheckFieldDTO? GetById(int checkFieldId, string issuerUserId)
        {
            var checkField = GetCheckFieldWith_Question_Section_Exam_Included(checkFieldId);

            if (checkField.Question.Section.Exam.CreatorUserId != issuerUserId
                && !checkField.Question.Section.Exam.ExamUsers.Any(x => x.UserId == issuerUserId))
                throw new ApplicationUnAuthorizedException($"User has no access to Question");

            return _checkFieldMapper.EntityToShowDTO(checkField);
        }

        public void Update(int checkFieldId, string issuerUserId, UpdateCheckFieldDTO dTO)
        {
            var checkField = GetCheckFieldWith_Question_Section_Exam_Included(checkFieldId);

            if (checkField.Question.Section.Exam.CreatorUserId != issuerUserId)
                throw new ApplicationUnAuthorizedException("User has no access to CheckField");

            _checkFieldValidator.ValidateDTO(dTO);

            _checkFieldMapper.UpdateEntityByDTO(checkField, dTO);

            _checkFieldInternalService.Update(checkField);
        }

        private CheckField GetCheckFieldWith_Question_Section_Exam_Included(int checkFieldId)
        {
            return _checkFieldInternalService.GetById(checkFieldId,
                _checkFieldInternalService.GetIQueryable()
                .Include(x => x.Question)
                .ThenInclude(x => x.Section)
                .ThenInclude(x => x.Exam));
        }
    }
}
