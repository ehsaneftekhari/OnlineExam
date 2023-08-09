using Microsoft.EntityFrameworkCore;
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

        public int Delete(Section entity)
        {
            _context.Remove(entity);
            return _context.SaveChanges();
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
