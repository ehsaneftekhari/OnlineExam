using OnlineExam.Application.Contract.DTOs.QuestionDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface IQuestionService
    {
        ShowQuestionDTO Add(AddQuestionDTO dTO);
        ShowQuestionDTO? GetById(int id);
        IEnumerable<ShowQuestionDTO> GetAllBySectionId(int sectionId, int skip, int take);
        void Update(UpdateQuestionDTO dTO);
        void Delete(int id);
    }
}
