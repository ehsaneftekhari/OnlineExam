using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class CheckFieldValidator : ICheckFieldValidator
    {
        readonly IQuestionInternalService _questionInternalService;
        readonly IExamValidator _examValidator;

        public CheckFieldValidator(IQuestionInternalService questionInternalService,
                                   IExamValidator examValidator)
        {
            _questionInternalService = questionInternalService;
            _examValidator = examValidator;
        }

        public void ValidateDTO(AddCheckFieldDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            ValidateValues(dTO.MaximumSelection, dTO.CheckFieldUIType);
        }

        public void ValidateDTO(UpdateCheckFieldDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            ValidateValues(dTO.MaximumSelection, dTO.CheckFieldUIType);
        }

        public void ThrowIfUserIsNotExamCreator(int questionId, string issuerUserId)
        {
            var question = GetQuestionWith_Section_Exam_Included(questionId);

            if (question.Section.Exam.CreatorUserId != issuerUserId)
                throw new ApplicationUnAuthorizedException("User has no access to Question");
        }

        public void ThrowIfUserIsNotExamCreatorOrExamUser(int questionId, string issuerUserId)
        {
            var question = GetQuestionWith_Section_Exam_ExamUser_Included(questionId);

            if (question.Section.Exam.CreatorUserId != issuerUserId
                && !question.Section.Exam.ExamUsers.Any(x => x.UserId == issuerUserId))
                throw new ApplicationUnAuthorizedException($"User has no access to Question");
        }

        public void ThrowIfUserIsNotExamCreatorOrExamUser(string issuerUserId, Exam exam)
        {
            if (exam.CreatorUserId != issuerUserId
                && !exam.ExamUsers.Any(x => x.UserId == issuerUserId))
                throw new ApplicationUnAuthorizedException($"User has no access to Question");
        }

        public void ThrowIfUserIsNotExamCreator(string issuerUserId, Exam exam)
        {
            _examValidator.ThrowIfUserIsNotExamCreator(issuerUserId, exam);
        }


        private void ValidateValues(int? maximumSelection, int? checkFieldUIType)
        {
            if (maximumSelection.HasValue && maximumSelection < 1)
                throw new ApplicationValidationException("maximumSelection can not be less then 1");

            if (checkFieldUIType.HasValue && !Enum.IsDefined(typeof(CheckFieldUIType), checkFieldUIType))
                throw new ApplicationValidationException("checkFieldUIType is not valid");
        }

        private Question GetQuestionWith_Section_Exam_Included(int questionId)
        {
            return _questionInternalService.GetById(questionId,
                            _questionInternalService.GetIQueryable()
                            .Include(x => x.Section)
                            .ThenInclude(x => x.Exam));
        }

        private Question GetQuestionWith_Section_Exam_ExamUser_Included(int questionId)
        {
            return _questionInternalService.GetById(questionId,
                            _questionInternalService.GetIQueryable()
                            .Include(x => x.Section)
                            .ThenInclude(x => x.Exam)
                            .ThenInclude(x => x.ExamUsers));
        }
    }
}
