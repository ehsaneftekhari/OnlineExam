using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class TextFieldValidator : ITextFieldValidator
    {
        public void ValidateDTO(AddTextFieldDTO dTO)
            => ValidateValues(dTO.AnswerLength, dTO.TextFieldUIType);

        public void ValidateDTO(UpdateTextFieldDTO dTO)
            => ValidateValues(dTO.AnswerLength, dTO.TextFieldUIType);

        private void ValidateValues(int? answerLength, int? textFieldUIType)
        {
            if (answerLength.HasValue && (answerLength < 1 || answerLength > 8000))
                throw new ApplicationValidationException("valid AnswerLength is from 1 to 8000");

            if (textFieldUIType.HasValue && !Enum.IsDefined(typeof(TextFieldUIType), textFieldUIType))
                throw new ApplicationValidationException("TextFieldUIType is not valid");
        }
    }
}
