using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        protected readonly OnlineExamContext _context;

        public AnswerRepository(OnlineExamContext context)
        {
            _context = context;
        }

        public int Add(Answer answer)
        {
            _context.Answer.Add(answer);
            return _context.SaveChanges();
        }

        public int Delete(Answer entity)
        {
            _context.Answer.Remove(entity);
            return _context.SaveChanges();
        }

        public Answer? GetById((int ExamUserId, int QuestionId) id)
        {
            return _context.Answer.FirstOrDefault(x => x.ExamUserId == id.Item1 && x.QuestionId == id.Item2);
        }

        public Answer? GetById(int ExamUserId, int QuestionId)
            => GetById((ExamUserId, QuestionId));

        public IQueryable<Answer> GetIQueryable()
        {
            return _context.Answer;
        }

        public int Update(Answer entity)
        {
            _context.Answer.Update(entity);
            return _context.SaveChanges();
        }
    }
}
