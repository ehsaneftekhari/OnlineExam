using Microsoft.EntityFrameworkCore;
using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class ExamRepository : BaseRepository<Exam>, IExamRepository
    {
        public ExamRepository(OnlineExamContext context) : base(context) { }

        public Exam? GetWithSectionsLoaded(int id)
        {
            return _context.Exam.Include(x => x.Sections).FirstOrDefault(x => x.Id == id);
        }
    }
}
