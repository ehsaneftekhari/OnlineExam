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

        public override FileField? GetById(int id)
        {
            return GetIQueryable().FirstOrDefault(x => x.Id == id);
        }

        public override IQueryable<FileField> GetIQueryable()
        {
            return _context.FileField.Include(x => x.AllowedFileTypes);
        }
    }
}
