using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    internal class ExamValidator : IExamValidator
    {

        public bool IsUserExamCreator(string issuerUserId, Exam exam)
            => exam.CreatorUserId != issuerUserId;

        public void ThrowIfUserIsNotExamCreator(string issuerUserId, Exam exam)
        {
            if (IsUserExamCreator(issuerUserId, exam))
                throw new ApplicationUnAuthorizedException("User has no access to Exam");
        }
    }
}
