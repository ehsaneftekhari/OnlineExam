using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class CheckFieldOptionValidator : ICheckFieldOptionValidator
    {
        readonly ICheckFieldInternalService _checkFieldInternalService;
        readonly IExamValidator _examValidator;

        public CheckFieldOptionValidator(ICheckFieldInternalService checkFieldInternalService,
                           IExamValidator examValidator)
        {
            _checkFieldInternalService = checkFieldInternalService;
            _examValidator = examValidator;
        }

        public void ValidateDTO(AddCheckFieldOptionDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            ValidateValues(dTO.Order, dTO.Text);
        }

        public void ValidateDTO(UpdateCheckFieldOptionDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            ValidateValues(dTO.Order, dTO.Text);
        }

        public void ThrowIfUserIsNotExamCreator(int questionId, string userId)
        {
            ThrowIfUserIsNotExamCreator(userId,
                GetCheckFieldWith_Question_Section_Exam_Included(questionId).Question.Section.Exam);
        }

        public void ThrowIfUserIsNotExamCreatorOrExamUser(int questionId, string userId)
        {
            ThrowIfUserIsNotExamCreatorOrExamUser(userId,
                GetCheckFieldWith_Question_Section_Exam_ExamUser_Included(questionId).Question.Section.Exam);
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

        private void ValidateValues(int? Order, string? Text)
        {
            if (Order.HasValue && Order < 1)
                throw new ApplicationValidationException("Order can not be less then 1");

            if (Text != null && Text!.Length > 4000)
                throw new ApplicationValidationException("Text length can not be more than 4000 characters");
        }

        private CheckField GetCheckFieldWith_Question_Section_Exam_Included(int questionId)
        {
            return _checkFieldInternalService.GetById(questionId,
                            _checkFieldInternalService.GetIQueryable()
                            .Include(x => x.Question)
                            .ThenInclude(x => x.Section)
                            .ThenInclude(x => x.Exam));
        }

        private CheckField GetCheckFieldWith_Question_Section_Exam_ExamUser_Included(int questionId)
        {
            return _checkFieldInternalService.GetById(questionId,
                            _checkFieldInternalService.GetIQueryable()
                            .Include(x => x.Question)
                            .ThenInclude(x => x.Section)
                            .ThenInclude(x => x.Exam)
                            .ThenInclude(x => x.ExamUsers));
        }
    }
}
