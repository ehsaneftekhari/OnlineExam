using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class SectionAccessValidator : ISectionAccessValidator
    {
        readonly IExamInternalService _examInternalService;
        readonly IExamValidator _examValidator;

        public SectionAccessValidator(IExamInternalService examInternalService, IExamValidator examValidator)
        {
            _examInternalService = examInternalService;
            _examValidator = examValidator;
        }

        public void ThrowIfUserIsNotExamCreator(string issuerUserId, int examId)
        {
            _examValidator.ThrowIfUserIsNotExamCreator(issuerUserId, _examInternalService.GetById(examId));
        }

        public void ThrowIfUserIsNotExamCreator(string issuerUserId, Exam exam)
        {
            _examValidator.ThrowIfUserIsNotExamCreator(issuerUserId, exam);
        }

        public void ThrowIfUserIsNotExamCreatorOrExamUserCreator(string userId, Exam exam)
        {
            if (!_examValidator.IsUserExamCreatorOrExamUser(userId, exam))
                throw new ApplicationUnAuthorizedException("User has no access to Section");
        }
    }
}
