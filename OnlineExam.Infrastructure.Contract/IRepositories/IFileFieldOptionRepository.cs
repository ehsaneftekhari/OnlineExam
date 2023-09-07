using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Contract.IRepositories
{
    public interface IFileFieldOptionRepository : IAddRepository<FileField>, IGetRepository<FileField>,
        IUpdateRepository<FileField>, IDeleteRepository<FileField>, IGetQueryableRepository<FileField>
    { }
}
