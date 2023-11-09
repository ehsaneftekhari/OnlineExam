using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IInternalService
{
    public interface IExamUserInternalService : IBaseInternalService<ExamUser, int, Exam, int>
    {

    }
}
