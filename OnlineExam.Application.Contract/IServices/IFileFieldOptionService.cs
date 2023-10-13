using OnlineExam.Application.Contract.DTOs.FileFieldDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface IFileFieldService
    {
        ShowFileFieldDTO Add(int questionId, AddFileFieldDTO dTO);
        ShowFileFieldDTO? GetById(int id);
        IEnumerable<ShowFileFieldDTO> GetAllByQuestionId(int questionId, int skip = 0, int take = 20);
        void Update(int id, UpdateFileFieldDTO dTO);
        void Delete(int id);
    }
}