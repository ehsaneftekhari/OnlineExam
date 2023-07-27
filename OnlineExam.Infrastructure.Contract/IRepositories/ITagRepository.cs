using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Contract.IRepositories
{
    public interface ITagRepository : IAddRepository<Tag>, IGetRepository<Tag>,
        IUpdateRepository<Tag>, IDeleteByIdRepository<Tag>, IGetQueryableRepository<Tag>
    {
    }
}
