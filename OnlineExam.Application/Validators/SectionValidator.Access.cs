using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class SectionAccessValidator : ISectionAccessValidator
    {
        readonly IExamInternalService _examInternalService;
        readonly IExamAccessValidator _examAccessValidator;

        public SectionAccessValidator(IExamInternalService examInternalService, IExamAccessValidator examAccessValidator)
        {
            _examInternalService = examInternalService;
            _examAccessValidator = examAccessValidator;
        }

        public void ThrowIfUserIsNotExamCreator(string issuerUserId, int examId)
        {
            _examAccessValidator.ThrowIfUserIsNotExamCreator(issuerUserId, _examInternalService.GetById(examId));
        }

        public void ThrowIfUserIsNotExamCreator(string issuerUserId, Exam exam)
        {
            _examAccessValidator.ThrowIfUserIsNotExamCreator(issuerUserId, exam);
        }

        public void ThrowIfUserIsNotExamCreatorOrExamUserCreator(string userId, Exam exam)
        {
            if (!_examAccessValidator.IsUserExamCreatorOrExamUser(userId, exam))
                throw new ApplicationUnAuthorizedException("User has no access to Section");
        }
    }
}
