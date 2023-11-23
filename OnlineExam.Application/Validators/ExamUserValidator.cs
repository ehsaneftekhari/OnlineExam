using Microsoft.Extensions.DependencyInjection;
using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.ExamUserDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Services.ExamServices;
using OnlineExam.Application.Services.UserServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class ExamUserValidator : IExamUserValidator
    {
        readonly Lazy<IExamInternalService> examInternalService;
        readonly Lazy<IExamUserInternalService> examUserInternalService;

        public ExamUserValidator(IServiceProvider serviceProvider)
        {
            examInternalService = new (() => serviceProvider.GetRequiredService<IExamInternalService>());
            examUserInternalService = new(() => serviceProvider.GetRequiredService<IExamUserInternalService>());
        }

        private IExamInternalService _examInternalService => examInternalService.Value;

        private IExamUserInternalService _examUserInternalService => examUserInternalService.Value;

        public void ValidateBeforeAdd(AddExamUserDTO dTO)
        {
            var exam = _examInternalService.GetById(dTO.ExamId);

            if (!exam.Published)
                throw new ApplicationValidationException($"Exam (id: {dTO.ExamId}) is not published");

            if (exam.End < DateTime.Now)
                throw new ApplicationValidationException($"the Exam (id: {dTO.ExamId}) has finished at {exam.End}");

            if (_examUserInternalService
                .GetIQueryable()
                .Where(x => x.UserId == dTO.UserId)
                .Where(x => x.ExamId == dTO.ExamId)
                .Any())
                throw new ApplicationValidationException($"User (id : {dTO.UserId}) has been registered at this Exam (id: {dTO.ExamId}) before");
        }

        public void ValidateIfExamUserCanFinish(string issuerUserId, ExamUser examUser)
        {
            if (examUser.End != null)
                throw new ApplicationValidationException($"The Exam ({examUser.Id}) has finished for User (id : {issuerUserId}) at {examUser.End}");

            var exam = _examInternalService.GetById(examUser.ExamId);

            var nowDateTime = DateTime.Now;

            if (exam.End < nowDateTime)
                throw new ApplicationValidationException($"The Exam ({examUser.Id}) is already finished");
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
