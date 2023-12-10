using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface ISectionAccessValidator
    {
        void ThrowIfUserIsNotExamCreator(string issuerUserId, Exam exam);
        void ThrowIfUserIsNotExamCreator(string issuerUserId, int examId);
        void ThrowIfUserIsNotExamCreatorOrExamUserCreator(string userId, Exam exam);
    }
}
