using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        OnlineExamContext _context;

        public SectionRepository(OnlineExamContext context)
        {
            _context = context;
        }

        public int Add(Section section)
        {   
            _context.Add(section);
            return _context.SaveChanges();
        }

        public int DeleteById(int id)
        {
            var exam = _context.Section.FirstOrDefault(x => x.Id == id);
            if (exam != null)
            {
                _context.Remove(exam);
                return _context.SaveChanges();
            }
            return 0;
        }

        public Section? GetById(int id)
        {
            return _context.Section.Include(x => x.Exam).FirstOrDefault(x => x.Id == id);
        }

        public int Update(Section section)
        {
            _context.Section.Update(section);
            return _context.SaveChanges();
        }
    }
}
