using Microsoft.EntityFrameworkCore;
using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class FileFieldRepository : BaseRepository<FileField>, IFileFieldRepository
    {
        public FileFieldRepository(OnlineExamContext context) : base(context) { }
    }
}
