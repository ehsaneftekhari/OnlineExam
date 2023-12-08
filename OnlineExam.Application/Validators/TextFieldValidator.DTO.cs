using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class TextFieldDTOValidator : ITextFieldDTOValidator
    {
        readonly ITextFieldUiTypeDTOValidator _uiTypeValidator;

        public TextFieldDTOValidator(ITextFieldUiTypeDTOValidator uiTypeValidator)
        {
            _uiTypeValidator = uiTypeValidator;
        }

        public void ValidateDTO(AddTextFieldDTO dTO)
            => ValidateValues(dTO.AnswerLength, dTO.TextFieldUITypeId);

        public void ValidateDTO(UpdateTextFieldDTO dTO)
            => ValidateValues(dTO.AnswerLength, dTO.TextFieldUITypeId);

        private void ValidateValues(int? answerLength, int? textFieldUIType)
        {
            if (answerLength.HasValue && (answerLength < 1 || answerLength > 8000))
                throw new ApplicationValidationException("valid AnswerLength is from 1 to 8000");

            if (textFieldUIType.HasValue)
                _uiTypeValidator.ThrowIfIdIsNotValid(textFieldUIType.Value);
        }
    }
}
