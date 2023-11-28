using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface ICheckFieldService
    {
        ShowCheckFieldDTO Add(int questionId, string issuerUserId, AddCheckFieldDTO dTO);
        ShowCheckFieldDTO? GetById(int id);
        IEnumerable<ShowCheckFieldDTO> GetAllByQuestionId(int questionId, int skip = 0, int take = 20);
        void Update(int id, UpdateCheckFieldDTO dTO);
        void Delete(int id);
    }
}