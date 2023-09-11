using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(OnlineExamContext context) : base(context) { }
    }
}
