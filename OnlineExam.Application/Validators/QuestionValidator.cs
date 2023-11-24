using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Services.SectionServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class QuestionValidator : IQuestionValidator
    {
        readonly ISectionInternalService _sectionInternalService;
        readonly IExamValidator _examValidator;

        public QuestionValidator(ISectionInternalService sectionInternalService,
                                 IExamValidator examValidator)
        {
            _sectionInternalService = sectionInternalService;
            _examValidator = examValidator;
        }

        public void ThrowIfUserIsNotExamCreator(int sectionId, string issuerUserId)
        {
            var exam = _sectionInternalService
                .GetById(sectionId, _sectionInternalService
                                        .GetIQueryable()
                                        .Include(x => x.Exam)).Exam;

            ThrowIfUserIsNotExamCreator(issuerUserId, exam);
        }

        public void ThrowIfUserIsNotExamCreatorOrExamUser(int sectionId, string issuerUserId)
        {
            var exam = _sectionInternalService
                .GetById(sectionId, _sectionInternalService
                                        .GetIQueryable()
                                        .Include(x => x.Exam)
                                        .ThenInclude(x => x.ExamUsers)).Exam;

            ThrowIfUserIsNotExamCreatorOrExamUser(issuerUserId, exam);
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
    }
}
