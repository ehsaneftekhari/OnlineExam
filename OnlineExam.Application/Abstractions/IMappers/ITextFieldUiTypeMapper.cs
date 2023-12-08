using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IMappers
{
    public interface ITextFieldUiTypeMapper
    {
        TextFieldUIType IdToEnum(int id);
        ShowTextFieldTypeDTO EnumToShowDTO(TextFieldUIType textFieldUIType);
    }
}