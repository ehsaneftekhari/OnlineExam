using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Contract.IRepositories
{
    public interface ICheckFieldRepository : IAddRepository<CheckField>, IGetRepository<CheckField, int>,
        IUpdateRepository<CheckField>, IDeleteRepository<CheckField>, IGetQueryableRepository<CheckField>
    { }
}
