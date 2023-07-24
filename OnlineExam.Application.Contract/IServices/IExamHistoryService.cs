using OnlineExam.Application.Contract.DTOs;
using OnlineExam.Application.Contract.Markers;

namespace OnlineExam.Application.Contract.IServices
{
    public interface IExamHistoryService : IApplicationContractMarker
    {
        IEnumerable<ShowExamHistoryDTO> GetById(int id);
    }
}
