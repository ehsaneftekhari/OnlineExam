using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class CheckFieldOptionRepository : ICheckFieldOptionRepository
    {
        OnlineExamContext _context;

        public CheckFieldOptionRepository(OnlineExamContext context)
        {
            _context = context;
        }
        public int Add(CheckFieldOption checkFieldOption)
        {
            _context.CheckFieldOption.Add(checkFieldOption);
            return _context.SaveChanges();
        }

        public int Delete(CheckFieldOption checkFieldOption)
        {
            _context.CheckFieldOption.Remove(checkFieldOption);
            return _context.SaveChanges();
        }

        public CheckFieldOption? GetById(int id)
        {
            return _context.CheckFieldOption.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<CheckFieldOption> GetIQueryable()
        {
            return _context.CheckFieldOption;
        }

        public int Update(CheckFieldOption checkFieldOption)
        {
            _context.CheckFieldOption.Update(checkFieldOption);
            return _context.SaveChanges();
        }
    }
}
