using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Contract.IRepositories
{
    public interface IExamRepository : IBaseRepository<Exam>
    {
        Exam? GetFullyLoaded(int id);
    }
}
