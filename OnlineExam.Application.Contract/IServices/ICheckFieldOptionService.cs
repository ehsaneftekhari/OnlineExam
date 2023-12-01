using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface ICheckFieldOptionService
    {
        ShowCheckFieldOptionDTO Add(int checkFieldId, string issuerUserId, AddCheckFieldOptionDTO dTO);
        ShowCheckFieldOptionDTO? GetById(int id, string issuerUserId);
        IEnumerable<ShowCheckFieldOptionDTO> GetAllByCheckFieldId(int checkFieldId, string issuerUserId, int skip = 0, int take = 20);
        void Update(int id, string issuerUserId, UpdateCheckFieldOptionDTO dTO);
        void Delete(int id, string issuerUserId);
    }
}