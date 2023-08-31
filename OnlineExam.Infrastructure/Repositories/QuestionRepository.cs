using Microsoft.EntityFrameworkCore;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        OnlineExamContext _context;

        public QuestionRepository(OnlineExamContext context)
        {
            _context = context;
        }

        public int Add(Question question)
        {
            _context.Question.Add(question);
            return _context.SaveChanges();
        }

        public int Delete(Question question)
        {
            _context.Question.Remove(question);
            return _context.SaveChanges();
        }

        public Question? GetById(int id)
        {
            return _context.Question.Include(x => x.Section).FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Question> GetIQueryable()
        {
            return _context.Question;
        }

        public int Update(Question question)
        {
            _context.Question.Update(question);
            return _context.SaveChanges();
        }
    }
}
