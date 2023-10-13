using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface ICheckFieldOptionService
    {
        ShowCheckFieldOptionDTO Add(int checkFieldId, AddCheckFieldOptionDTO dTO);
        ShowCheckFieldOptionDTO? GetById(int id);
        IEnumerable<ShowCheckFieldOptionDTO> GetAllByCheckFieldId(int checkFieldId, int skip = 0, int take = 20);
        void Update(int id, UpdateCheckFieldOptionDTO dTO);
        void Delete(int id);
    }
}