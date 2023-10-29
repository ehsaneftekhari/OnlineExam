using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.InternalService
{
    public interface IAllowedFileTypesFieldInternalService : IBaseInternalService<AllowedFileTypesField>
    {
        internal IEnumerable<AllowedFileTypesField> GetByIds(IEnumerable<int> allowedFileTypesFieldIds);
    }
}
