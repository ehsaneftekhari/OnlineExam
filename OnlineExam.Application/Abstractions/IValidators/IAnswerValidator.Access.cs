using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IAnswerAccessValidator
    {
        bool IsUserExamUserOrExamCreator(ExamUser examUser, string userId);

        bool IsUserExamCreator(ExamUser examUser, string userId);
    }
}