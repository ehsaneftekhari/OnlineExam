using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IExamValidator
    {
        bool IsUserExamCreator(string issuerUserId, Exam exam);

        void ThrowIfUserIsNotExamCreator(string issuerUserId, Exam exam);
    }
}