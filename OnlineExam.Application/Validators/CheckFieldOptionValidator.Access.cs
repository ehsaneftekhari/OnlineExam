using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class CheckFieldOptionAccessValidator : ICheckFieldOptionAccessValidator
    {
        readonly ICheckFieldInternalService _checkFieldInternalService;
        readonly IExamValidator _examValidator;

        public CheckFieldOptionAccessValidator(ICheckFieldInternalService checkFieldInternalService,
                                               IExamValidator examValidator)
        {
            _checkFieldInternalService = checkFieldInternalService;
            _examValidator = examValidator;
        }

        public void ThrowIfUserIsNotExamCreator(int checkFieldId, string userId)
        {
            ThrowIfUserIsNotExamCreator(userId,
                GetCheckFieldWith_Question_Section_Exam_Included(checkFieldId).Question.Section.Exam);
        }

        public void ThrowIfUserIsNotExamCreatorOrExamUser(int checkFieldId, string userId)
        {
            ThrowIfUserIsNotExamCreatorOrExamUser(userId,
                GetCheckFieldWith_Question_Section_Exam_ExamUser_Included(checkFieldId).Question.Section.Exam);
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
