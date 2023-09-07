using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class FileFieldOptionRepository : BaseRepository<FileField>, IFileFieldOptionRepository
    {
        public FileFieldOptionRepository(OnlineExamContext context) : base(context) { }
    }
}
