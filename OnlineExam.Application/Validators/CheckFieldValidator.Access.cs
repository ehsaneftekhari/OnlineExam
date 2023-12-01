using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class CheckFieldAccessValidator : ICheckFieldAccessValidator
    {
        readonly IQuestionInternalService _questionInternalService;
        readonly IExamValidator _examValidator;

        public CheckFieldAccessValidator(IQuestionInternalService questionInternalService,
                                   IExamValidator examValidator)
        {
            _questionInternalService = questionInternalService;
            _examValidator = examValidator;
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
                throw new ApplicationUnAuthorizedException($"User has no access to CheckField");
        }

        public void ThrowIfUserIsNotExamCreator(string userId, Exam exam)
        {
            _examValidator.ThrowIfUserIsNotExamCreator(userId, exam);
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
