using OnlineExam.Application.Contract.DTOs.QuestionDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface IQuestionService
    {
        ShowQuestionDTO Add(int sectionId, string issuerUserId, AddQuestionDTO dTO);
        ShowQuestionDTO? GetById(int id, string issuerUserId);
        IEnumerable<ShowQuestionDTO> GetAllBySectionId(int sectionId, string issuerUserId, int skip, int take);
        void Update(int id, UpdateQuestionDTO dTO);
        void Delete(int id);
    }
}
