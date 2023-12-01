using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface ICheckFieldOptionAccessValidator
    {
        void ThrowIfUserIsNotExamCreator(int checkFieldId, string userId);
        void ThrowIfUserIsNotExamCreatorOrExamUser(int checkFieldId, string userId);
        void ThrowIfUserIsNotExamCreatorOrExamUser(string userId, Exam exam);
        void ThrowIfUserIsNotExamCreator(string userId, Exam exam);
    }
}