using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Contract.IRepositories
{
    public interface IFileFieldRepository : IAddRepository<FileField>, IGetRepository<FileField, int>,
        IUpdateRepository<FileField>, IDeleteRepository<FileField>, IGetQueryableRepository<FileField>
    { }
}
