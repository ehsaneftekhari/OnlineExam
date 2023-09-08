using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class AllowedFileTypesFieldOptionRepository : BaseRepository<AllowedFileTypesField>, IAllowedFileTypesFieldOptionRepository
    {
        public AllowedFileTypesFieldOptionRepository(OnlineExamContext context) : base(context) { }
    }
}
