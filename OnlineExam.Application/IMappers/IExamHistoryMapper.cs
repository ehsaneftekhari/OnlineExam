using OnlineExam.Application.Contract.DTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.IMappers
{
    public interface IExamHistoryMapper
    {
        public ShowExamHistoryDTO EntityDTO(ExamHistory examHistory);
    }
}
