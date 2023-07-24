using OnlineExam.Application.Contract.DTOs;
using OnlineExam.Application.IMappers;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Mappers
{
    internal class ExamHistoryMapper : IExamHistoryMapper
    {
        public ShowExamHistoryDTO EntityDTO(ExamHistory examHistory)
        {
            return new()
            {
                Title = examHistory.Title,
                Start = examHistory.Start,
                End = examHistory.End,
                Published = examHistory.Published,
                PeriodStart = examHistory.PeriodStart,
                PeriodEnd = examHistory.PeriodEnd
            };
        }
    }
}
