using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Mappers
{
    internal class TextFieldUiTypeMapper : ITextFieldUiTypeMapper
    {
        readonly ITextFieldUiTypeDTOValidator validator;

        public TextFieldUiTypeMapper(ITextFieldUiTypeDTOValidator validator)
        {
            this.validator = validator;
        }

        public TextFieldUIType IdToEnum(int id)
             => (TextFieldUIType)id;

        public ShowTextFieldTypeDTO EnumToShowDTO(TextFieldUIType textFieldUIType)
            => new ShowTextFieldTypeDTO()
            {
                Id = (int)textFieldUIType,
                Name = Enum.GetName(typeof(TextFieldUIType), textFieldUIType)!
            };
    }
}
