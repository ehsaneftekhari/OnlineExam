using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IInternalService
{
    public interface IQuestionInternalService : IBaseInternalService<Question, int, Section, int>
    {

    }
}
