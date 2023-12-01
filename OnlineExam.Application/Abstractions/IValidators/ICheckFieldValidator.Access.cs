using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface ICheckFieldAccessValidator
    {
        void ThrowIfUserIsNotExamCreator(int questionId, string issuerUserId);
        void ThrowIfUserIsNotExamCreatorOrExamUser(int questionId, string issuerUserId);
        void ThrowIfUserIsNotExamCreatorOrExamUser(string issuerUserId, Exam exam);
        void ThrowIfUserIsNotExamCreator(string issuerUserId, Exam exam);
    }
}