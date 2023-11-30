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

        public void ThrowIfUserIsNotExamCreator(int questionId, string userId)
        {
            ThrowIfUserIsNotExamCreator(userId, 
                GetQuestionWith_Section_Exam_Included(questionId).Section.Exam);
        }

        public void ThrowIfUserIsNotExamCreatorOrExamUser(int questionId, string userId)
        {
            ThrowIfUserIsNotExamCreatorOrExamUser(userId, 
                GetQuestionWith_Section_Exam_ExamUser_Included(questionId).Section.Exam);
        }

        public void ThrowIfUserIsNotExamCreatorOrExamUser(string userId, Exam exam)
        {
            if (!_examValidator.IsUserExamCreatorOrExamUser(userId, exam))
                throw new ApplicationUnAuthorizedException($"User has no access to Question");
        }

        public void ThrowIfUserIsNotExamCreator(string userId, Exam exam)
        {
            _examValidator.ThrowIfUserIsNotExamCreator(userId, exam);
        }


        private void ValidateValues(int? maximumSelection, int? checkFieldUIType)
        {
            if (maximumSelection.HasValue && maximumSelection < 1)
                throw new ApplicationValidationException("maximumSelection can not be less than 1");

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
