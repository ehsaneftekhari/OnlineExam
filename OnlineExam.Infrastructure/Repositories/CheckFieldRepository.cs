using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class CheckFieldRepository : BaseRepository<CheckField>, ICheckFieldRepository
    {
        public CheckFieldRepository(OnlineExamContext context) : base(context) { }
    }
}
