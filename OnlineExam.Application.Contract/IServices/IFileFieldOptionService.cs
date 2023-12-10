using OnlineExam.Application.Contract.DTOs.FileFieldDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface IFileFieldService
    {
        ShowFileFieldDTO Add(int questionId, string issuerUserId, AddFileFieldDTO dTO);
        ShowFileFieldDTO? GetById(int id, string issuerUserId);
        IEnumerable<ShowFileFieldDTO> GetAllByQuestionId(int questionId, string issuerUserId, int skip = 0, int take = 20);
        void Update(int id, string issuerUserId, UpdateFileFieldDTO dTO);
        void Delete(int id, string issuerUserId);
    }
}