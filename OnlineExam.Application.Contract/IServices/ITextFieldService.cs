using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface ITextFieldService
    {
        ShowTextFieldDTO Add(int questionId, AddTextFieldDTO dTO);
        ShowTextFieldDTO? GetById(int id);
        IEnumerable<ShowTextFieldDTO> GetAllByExamId(int questionId, int skip = 0, int take = 20);
        void Update(int id, UpdateTextFieldDTO dTO);
    }
}