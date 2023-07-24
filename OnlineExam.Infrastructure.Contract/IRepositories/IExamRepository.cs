using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Contract.IRepositories
{
    public interface IExamRepository : IAddRepository<Exam>, IGetRepository<Exam>, 
        IUpdateRepository<Exam>, IDeleteRepository<Exam>
    {

    }
}
