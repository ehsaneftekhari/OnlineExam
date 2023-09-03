using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface ITextFieldService
    {
        ShowTextFieldDTO Add(int questionId, AddTextFieldDTO dTO);
    }
}