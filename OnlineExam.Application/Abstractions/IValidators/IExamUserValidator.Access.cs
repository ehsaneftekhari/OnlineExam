using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IExamUserAccessValidator
    {
        void ThrowIfUserIsNotCreatorOfExamUserOrExam(string issuerUserId, ExamUser examUser);
        void ThrowIfUserIsNotCreatorOfExamUser(string issuerUserId, ExamUser examUser);
        void ThrowIfUserIsNotCreatorOfExam(int examId, string issuerUserId);
    }
}