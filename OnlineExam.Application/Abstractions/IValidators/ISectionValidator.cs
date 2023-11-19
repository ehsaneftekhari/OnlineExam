using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface ISectionValidator
    {
        void ThrowIfUserIsNotExamCreator(string issuerUserId, Exam exam);
        void ThrowIfUserIsNotExamCreator(string issuerUserId, int examId);
        void ThrowIfUserIsNotExamCreatorOrExamUserCreator(string userId, Section section);
    }
}
