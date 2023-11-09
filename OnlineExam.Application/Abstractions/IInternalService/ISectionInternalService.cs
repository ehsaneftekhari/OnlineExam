using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IInternalService
{
    public interface ISectionInternalService : IBaseInternalService<Section, int, Exam, int>
    {

    }
}
