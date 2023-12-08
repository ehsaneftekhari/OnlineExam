using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Mappers
{
    internal class TextFieldMapper : ITextFieldMapper
    {
        readonly ITextFieldUiTypeMapper _typeMapper;

        public TextFieldMapper(ITextFieldUiTypeMapper typeMapper)
        {
            _typeMapper = typeMapper;
        }

        public TextField? AddDTOToEntity(int questionId, AddTextFieldDTO? addTextFieldDTO)
        {
            if (addTextFieldDTO != null)
            {
                return new()
                {
                    QuestionId = questionId,
                    TextFieldUIType = _typeMapper.IdToEnum(addTextFieldDTO.TextFieldUITypeId),
                    AnswerLength = addTextFieldDTO.AnswerLength,
                    RegEx = addTextFieldDTO.RegEx
                };
            }
            return null;
        }

        public ShowTextFieldDTO EntityToShowDTO(TextField entity)
        {
            if (entity != null)
            {
                return new()
                {
                    Id = entity.Id,
                    UIType = _typeMapper.EnumToShowDTO(entity.TextFieldUIType),
                    QuestionId = entity.QuestionId,
                    AnswerLength = entity.AnswerLength,
                    RegEx = entity.RegEx
                };
            }

            return null;
        }

        public void UpdateEntityByDTO(TextField old, UpdateTextFieldDTO @new)
        {
            if (@new != null || old != null)
            {
                if (@new!.RegEx != null)
                    old.RegEx = @new.RegEx;

                if (@new!.AnswerLength != null)
                    old.AnswerLength = @new.AnswerLength;

                if (@new!.TextFieldUITypeId != null)
                    old.TextFieldUIType = (TextFieldUIType)@new.TextFieldUITypeId;
            }
        }
    }
}
