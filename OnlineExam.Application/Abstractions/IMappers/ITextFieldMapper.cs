using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IMappers
{
    public interface ITextFieldMapper
    {
        TextField? AddDTOToEntity(int questionId, AddTextFieldDTO? addTextFieldDTO);
        ShowTextFieldDTO EntityToShowDTO(TextField addTextFieldDTO);
        void UpdateEntityByDTO(TextField old, UpdateTextFieldDTO @new);
    }
}
