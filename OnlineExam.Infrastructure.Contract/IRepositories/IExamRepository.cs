using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Contract.IRepositories
{
    public interface IExamRepository : IAddRepository<Exam>, IGetByIdRepository<int, Exam>, 
        IUpdateRepository<Exam>, IDeleteByEntityRepository<Exam>, IGetQueryableRepository<Exam>
    {
        Exam? GetFullyLoaded(int id);
    }
}
