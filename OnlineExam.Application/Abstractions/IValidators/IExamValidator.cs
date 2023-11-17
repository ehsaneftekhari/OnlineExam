using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IExamValidator
    {
        void ThrowIfUserIsNotExamCreator(string issuerUserId, Exam exam);
    }
}