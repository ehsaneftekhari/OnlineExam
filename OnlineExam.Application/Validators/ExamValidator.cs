using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    internal class ExamValidator : IExamValidator
    {
        private void ThrowIfUserIsNotExamCreator(string issuerUserId, Exam exam)
        {
            if (exam.CreatorUserId != issuerUserId)
                throw new ApplicationUnAuthorizedException(
                    string.Empty,
                    new ApplicationValidationException(GenerateUnAuthorizedExceptionMessage(exam.Id, issuerUserId)));
        }

        private string GenerateUnAuthorizedExceptionMessage(int examId, string issuerUserId)
             => $"User (id : {issuerUserId}) is not the owner of exam (id : {examId})";
    }
}
