using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Contract.IRepositories
{
    public interface ITextFieldRepository : IAddRepository<TextField>, IGetRepository<TextField>,
        IUpdateRepository<TextField>, IDeleteRepository<TextField>, IGetQueryableRepository<TextField>
    { }
}
