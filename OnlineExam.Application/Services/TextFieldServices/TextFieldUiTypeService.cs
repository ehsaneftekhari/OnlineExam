using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.TextFieldServices
{
    public class TextFieldUiTypeService : ITextFieldUiTypeService
    {
        readonly ITextFieldUiTypeMapper _textFieldFieldTypeMapper;
        readonly ITextFieldUiTypeDTOValidator _textFieldUiTypeValidator;

        public TextFieldUiTypeService(ITextFieldUiTypeMapper textFieldFieldTypeMapper, ITextFieldUiTypeDTOValidator textFieldUiTypeValidator)
        {
            _textFieldFieldTypeMapper = textFieldFieldTypeMapper;
            _textFieldUiTypeValidator = textFieldUiTypeValidator;
        }

        public ShowTextFieldTypeDTO? GetById(int textFieldUITypeId)
        {
            _textFieldUiTypeValidator.ThrowIfIdIsNotValid(textFieldUITypeId);

            return _textFieldFieldTypeMapper.EnumToShowDTO(_textFieldFieldTypeMapper.IdToEnum(textFieldUITypeId));
        }

        public IEnumerable<ShowTextFieldTypeDTO> GetAll()
             => Enum.GetValues<TextFieldUIType>().Select(x => _textFieldFieldTypeMapper.EnumToShowDTO(x));
    }
}
