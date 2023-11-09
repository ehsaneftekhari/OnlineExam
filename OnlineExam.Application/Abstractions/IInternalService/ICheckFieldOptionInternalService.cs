using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IInternalService
{
    public interface ICheckFieldOptionInternalService : IBaseInternalService<CheckFieldOption, int, Question, int>
    {

    }
}
