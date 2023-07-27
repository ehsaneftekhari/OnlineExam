using OnlineExam.Application.Contract.DTOs.TagDTOs;
using OnlineExam.Application.Contract.Markers;

namespace OnlineExam.Application.Contract.IServices
{
    public interface ITagService : IApplicationContractMarker
    {
        bool Add(AddTagDTO dTO);
        IEnumerable<ShowTagDTO> GetOrAdd(IEnumerable<AddTagDTO> dTOs);
        ShowTagDTO GetOrAdd(AddTagDTO dTO);
        bool Delete(int id);
        ShowTagDTO? GetById(int id);
        bool Update(UpdateTagDTO dTO);
    }
}