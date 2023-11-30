using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    internal class ExamValidator : IExamValidator
    {
        public void ThrowIfUserIsNotExamCreator(string issuerUserId, Exam exam)
        {
            if (exam.CreatorUserId != issuerUserId)
                throw new ApplicationUnAuthorizedException("User has no access to Exam");
        }
    }
}
