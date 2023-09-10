using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Contract.IRepositories
{
    public interface IExamUserRepository : IAddRepository<ExamUser>, IGetRepository<ExamUser>,
        IUpdateRepository<ExamUser>, IDeleteRepository<ExamUser>, IGetQueryableRepository<ExamUser>
    { }
}
