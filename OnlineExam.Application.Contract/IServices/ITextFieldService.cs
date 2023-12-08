using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface ITextFieldService
    {
        ShowTextFieldDTO Add(int questionId, string issuerUserId, AddTextFieldDTO dTO);
        ShowTextFieldDTO? GetById(int id, string issuerUserId);
        IEnumerable<ShowTextFieldDTO> GetAllByQuestionId(int questionId, string issuerUserId, int skip = 0, int take = 20);
        void Update(int id, string issuerUserId, UpdateTextFieldDTO dTO);
        void Delete(int id, string issuerUserId);
    }
}