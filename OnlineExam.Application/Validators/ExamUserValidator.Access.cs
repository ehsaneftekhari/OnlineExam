using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class ExamUserAccessValidator : IExamUserAccessValidator
    {
        readonly IExamInternalService _examInternalService;

        public ExamUserAccessValidator(IExamInternalService examInternalService)
        {
            _examInternalService = examInternalService;
        }

        public void ThrowIfUserIsNotCreatorOfExamUserOrExam(string issuerUserId, ExamUser examUser)
        {
            if (!IsCreatorOfExamUser(examUser, issuerUserId) && !IsCreatorOfExam(examUser.ExamId, issuerUserId))
                throw new ApplicationUnAuthorizedException(
                string.Empty,
                new ApplicationValidationException(GenerateUnAuthorizedExceptionMessage(issuerUserId)));
        }

        public void ThrowIfUserIsNotCreatorOfExamUser(string issuerUserId, ExamUser examUser)
        {
            if (IsCreatorOfExamUser(examUser, issuerUserId))
                throw new ApplicationUnAuthorizedException(
                string.Empty,
                new ApplicationValidationException(GenerateUnAuthorizedExceptionMessage(issuerUserId)));
        }

        public void ThrowIfUserIsNotCreatorOfExam(int examId, string issuerUserId)
        {
            if (!IsCreatorOfExam(examId, issuerUserId))
                throw new ApplicationUnAuthorizedException(
                    string.Empty,
                    new ApplicationValidationException(GenerateUnAuthorizedExceptionMessage(examId, issuerUserId)));
        }

        private bool IsCreatorOfExam(int examId, string userId)
            => _examInternalService.GetById(examId).CreatorUserId == userId;

        private bool IsCreatorOfExamUser(ExamUser examUser, string userId)
            => examUser.UserId == userId;

        private string GenerateUnAuthorizedExceptionMessage(string issuerUserId)
            => $"User (id : {issuerUserId}) has no access to this ExamUser";

        private string GenerateUnAuthorizedExceptionMessage(int examId, string issuerUserId)
            => $"User (id : {issuerUserId}) is not the owner of exam (id : {examId})";
    }
}
