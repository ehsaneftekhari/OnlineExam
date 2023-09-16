using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Contract.IRepositories
{
    public interface ICheckFieldOptionRepository : IAddRepository<CheckFieldOption>, IGetRepository<CheckFieldOption, int>,
        IUpdateRepository<CheckFieldOption>, IDeleteRepository<CheckFieldOption>, IGetQueryableRepository<CheckFieldOption>
    { }
}
