using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IExamUserActionValidator
    {
        void ValidateIfExamUserCanFinish(ExamUser examUser);
    }
}