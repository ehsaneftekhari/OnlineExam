using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;
using OnlineExam.Application.IMappers;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Mappers
{
    internal class TextFieldMapper : ITextFieldMapper
    {
        public TextField? AddDTOToEntity(int questionId, AddTextFieldDTO? addTextFieldDTO)
        {
            if (addTextFieldDTO != null)
            {
                return new()
                {
                    QuestionId = questionId,
                    TextFieldUIType = (TextFieldUIType)addTextFieldDTO.TextFieldUIType,
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
                    TextFieldUIType = (int)entity.TextFieldUIType,
                    TextFieldUITypeName = Enum.GetName(typeof(TextFieldUIType), entity.TextFieldUIType)!,
                    QuestionId = entity.QuestionId,
                    AnswerLength = entity.AnswerLength,
                    RegEx = entity.RegEx
                };
            }

            return null;
        }
    }
}
