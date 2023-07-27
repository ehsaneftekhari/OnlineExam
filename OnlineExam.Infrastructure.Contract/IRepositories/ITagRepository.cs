using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Contract.IRepositories
{
    public interface ITagRepository : IAddRepository<Tag>, IGetByIdRepository<int, Tag>,
        IUpdateRepository<Tag>, IDeleteByIdRepository<int, Tag>, IGetQueryableRepository<Tag>
    {
        Tag? GetByName(string name);
    }
}
