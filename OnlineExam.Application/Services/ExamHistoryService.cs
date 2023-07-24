using OnlineExam.Application.Contract.DTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.IMappers;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services
{
    public class ExamHistoryService : IExamHistoryService
    {
        readonly IExamHistoryRepository _repository;
        readonly IExamHistoryMapper _mapper;

        public ExamHistoryService(IExamHistoryRepository repository, IExamHistoryMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<ShowExamHistoryDTO> GetById(int id)
        {
            return _repository
                .GetAllById(id)
                .ToList()
                .Select(eh => MappExamHistoryToShowDTO(eh));

        }

        private ShowExamHistoryDTO MappExamHistoryToShowDTO(ExamHistory examHistory)
        {
            var dto = _mapper.EntityDTO(examHistory);
            dto.PeriodEnd = dto.PeriodEnd < DateTime.Now ? dto.PeriodEnd : null;
            dto.Period = dto.PeriodEnd.HasValue ? dto.PeriodEnd.Value - dto.PeriodStart : DateTime.Now - dto.PeriodStart;
            return dto;
        }
    }
}
