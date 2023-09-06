using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class CheckFieldOptionRepository : BaseRepository<CheckFieldOption>, ICheckFieldOptionRepository
    {
        public CheckFieldOptionRepository(OnlineExamContext context) : base(context) { }
    }
}
