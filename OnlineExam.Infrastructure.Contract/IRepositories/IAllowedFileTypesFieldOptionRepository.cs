using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Contract.IRepositories
{
    public interface IAllowedFileTypesFieldOptionRepository : IAddRepository<AllowedFileTypesField>, IGetRepository<AllowedFileTypesField>,
        IUpdateRepository<AllowedFileTypesField>, IDeleteRepository<AllowedFileTypesField>, IGetQueryableRepository<AllowedFileTypesField>
    { }
}
