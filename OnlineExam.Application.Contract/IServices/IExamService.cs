using OnlineExam.Application.Contract.DTOs;
using OnlineExam.Application.Contract.Markers;

namespace OnlineExam.Application.Contract.IServices
{
    public interface IExamService : IApplicationContractMarker
    {
        bool Add(AddExamDTO dTO);

        ShowExamDTO? GetById(int id);

        bool Update(UpdateExamDTO dTO);

        bool Delete(int id);
    }
}
