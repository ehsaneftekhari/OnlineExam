using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class QuestionAccessValidator : IQuestionAccessValidator
    {
        readonly ISectionInternalService _sectionInternalService;
        readonly IExamValidator _examValidator;

        public QuestionAccessValidator(ISectionInternalService sectionInternalService,
                                 IExamValidator examValidator)
        {
            _sectionInternalService = sectionInternalService;
            _examValidator = examValidator;
        }

        public void ThrowIfUserIsNotExamCreator(int sectionId, string userId)
        {
            var exam = _sectionInternalService
                .GetById(sectionId, _sectionInternalService
                                        .GetIQueryable()
                                        .Include(x => x.Exam)).Exam;

            ThrowIfUserIsNotExamCreator(userId, exam);
        }

        public void ThrowIfUserIsNotExamCreatorOrExamUser(int sectionId, string userId)
        {
            var exam = _sectionInternalService
                .GetById(sectionId, _sectionInternalService
                                        .GetIQueryable()
                                        .Include(x => x.Exam)
                                        .ThenInclude(x => x.ExamUsers)).Exam;

            ThrowIfUserIsNotExamCreatorOrExamUser(userId, exam);
        }

        public void ThrowIfUserIsNotExamCreatorOrExamUser(string userId, Exam exam)
        {
            if (!_examValidator.IsUserExamCreatorOrExamUser(userId, exam))
                throw new ApplicationUnAuthorizedException($"User has no access to Question");
        }

        public void ThrowIfUserIsNotExamCreator(string issuerUserId, Exam exam)
        {
            _examValidator.ThrowIfUserIsNotExamCreator(issuerUserId, exam);
        }
    }
}
