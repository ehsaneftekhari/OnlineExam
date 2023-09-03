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
                return new()
                {
                    QuestionId = questionId,
                    TextFieldUIType = addTextFieldDTO!.TextFieldUIType,
                    AnswerLength = addTextFieldDTO.AnswerLength,
                    RegEx = addTextFieldDTO.RegEx
                };

            return null;
        }

        public ShowTextFieldDTO EntityToShowDTO(TextField addTextFieldDTO)
        {
            if (addTextFieldDTO != null)
            {
                return new()
                {
                    Id = addTextFieldDTO.Id,
                    TextFieldUIType = addTextFieldDTO.TextFieldUIType,
                    QuestionId = addTextFieldDTO.QuestionId,
                    AnswerLength = addTextFieldDTO.AnswerLength,
                    RegEx = addTextFieldDTO.RegEx
                };
            }

            return null;
        }
    }
}
