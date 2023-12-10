using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IInternalService
{
    public interface IAllowedFileTypesFieldInternalService : IBaseInternalService<AllowedFileTypesField, int>
    {
        internal IEnumerable<AllowedFileTypesField> GetByIds(IEnumerable<int> allowedFileTypesFieldIds);
    }
}
