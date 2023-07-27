using OnlineExam.Application.Contract.DTOs.TagDTOs;
using OnlineExam.Application.Contract.Markers;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Contract.IServices
{
    public interface ITagService : IApplicationContractMarker
    {
        bool Add(AddTagDTO dTO);
        IEnumerable<Tag> SyncTagsByNames(IEnumerable<Tag> dTOs);
        Tag SyncTagByName(Tag dTO);
        bool DeleteById(int Id);
        ShowTagDetailsDTO? GetById(int Id);
        bool Update(UpdateTagDTO dTO);
    }
}