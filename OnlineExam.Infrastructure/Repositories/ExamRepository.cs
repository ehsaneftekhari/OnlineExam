using Microsoft.EntityFrameworkCore;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class ExamRepository : IExamRepository
    {
        OnlineExamContext _context;

        public ExamRepository(OnlineExamContext context)
        {
            _context = context;
        }

        public int Add(Exam exam)
        {
            _context.Add(exam);
            var tState = _context.Entry<Exam>(exam).State;
            return _context.SaveChanges();
        }

        public int DeleteByEntity(Exam exam)
        {
            if (exam != null)
            {
                _context.Remove(exam);
                return _context.SaveChanges();
            }
            return 0;
        }

        public Exam? GetWithSectionsLoaded(int id)
        {
            return _context.Exam.Include(x => x.Sections).FirstOrDefault(x => x.Id == id);
        }

        public Exam? GetById(int id)
        {
            return _context.Exam.FirstOrDefault(x => x.Id == id);
        }

        public int Update(Exam exam)
        {
            _context.Exam.Update(exam);
            var tState = _context.Entry<Exam>(exam).State;
            return _context.SaveChanges();
        }
    }
}
