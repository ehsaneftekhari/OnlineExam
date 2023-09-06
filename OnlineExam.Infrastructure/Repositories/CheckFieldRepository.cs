using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class CheckFieldRepository : ICheckFieldRepository
    {
        OnlineExamContext _context;

        public CheckFieldRepository(OnlineExamContext context)
        {
            _context = context;
        }

        public int Add(CheckField checkField)
        {
            _context.CheckField.Add(checkField);
            return _context.SaveChanges();
        }

        public int Delete(CheckField checkField)
        {
            _context.CheckField.Remove(checkField);
            return _context.SaveChanges();
        }

        public CheckField? GetById(int id)
        {
            return _context.CheckField.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<CheckField> GetIQueryable()
        {
            return _context.CheckField;
        }

        public int Update(CheckField checkField)
        {
            _context.CheckField.Update(checkField);
            return _context.SaveChanges();
        }
    }
}
