using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    internal class ExamAccessValidator : IExamAccessValidator
    {
        public bool IsUserExamCreator(string userId, Exam exam)
        {
            if (exam == null)
                throw new ArgumentNullException(nameof(exam));

            return exam.CreatorUserId == userId;
        }

        public bool IsUserExamCreatorOrExamUser(string userId, Exam exam)
        {
            return IsUserExamCreator(userId, exam) || exam.ExamUsers.Any(x => x.UserId == userId);
        }

        public void ThrowIfUserIsNotExamCreator(string userId, Exam exam)
        {
            if (!IsUserExamCreator(userId, exam))
                throw new ApplicationUnAuthorizedException("User has no access to Exam");
        }
    }
}
