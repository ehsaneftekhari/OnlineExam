using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class ExamUserRepository : BaseRepository<ExamUser>, IExamUserRepository
    {
        public ExamUserRepository(OnlineExamContext context) : base(context) { }
    }
}
