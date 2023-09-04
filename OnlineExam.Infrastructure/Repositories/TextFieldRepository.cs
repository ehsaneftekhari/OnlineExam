using Microsoft.EntityFrameworkCore;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class TextFieldRepository : ITextFieldRepository
    {
        OnlineExamContext _context;

        public TextFieldRepository(OnlineExamContext context)
        {
            _context = context;
        }

        public int Add(TextField textField)
        {
            _context.TextField.Add(textField);
            return _context.SaveChanges();
        }

        public int Delete(TextField textField)
        {
            _context.TextField.Remove(textField);
            return _context.SaveChanges();
        }

        public TextField? GetById(int id)
        {
            return _context.TextField.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<TextField> GetIQueryable()
        {
            return _context.TextField;
        }

        public int Update(TextField textField)
        {
            _context.TextField.Update(textField);
            return _context.SaveChanges();
        }
    }
}
