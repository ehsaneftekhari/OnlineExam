using Microsoft.EntityFrameworkCore;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class ExamHistoryRepository : IExamHistoryRepository
    {
        OnlineExamContext _context;

        public ExamHistoryRepository(OnlineExamContext context)
        {
            _context = context;
        }

        public IQueryable<ExamHistory> GetAllById(int id)
        {
            return _context.Exam.TemporalAll()
                .Where(e => e.Id == id)
                .Select(e => 
                    new ExamHistory
                    {
                        CreatorUserId = e.CreatorUserId,
                        Id = e.Id,
                        Title = e.Title,
                        Start = e.Start,
                        End = e.End,
                        Published = e.Published,
                        PeriodStart = EF.Property<DateTime>(e, "PeriodStart").ToLocalTime(),
                        PeriodEnd = EF.Property<DateTime>(e, "PeriodEnd").ToLocalTime()
                    }
                );
        }
    }

}
