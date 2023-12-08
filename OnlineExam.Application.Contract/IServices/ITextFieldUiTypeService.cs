using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface ITextFieldUiTypeService
    {
        IEnumerable<ShowTextFieldTypeDTO> GetAll();
        ShowTextFieldTypeDTO? GetById(int textFieldUITypeId);
    }
}