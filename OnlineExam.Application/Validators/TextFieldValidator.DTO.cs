using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class TextFieldDTOValidator : ITextFieldDTOValidator
    {
        readonly ITextFieldUiTypeDTOValidator _uiTypeValidator;

        public int AnswerMaxLength { get; init; }

        public TextFieldDTOValidator(ITextFieldUiTypeDTOValidator uiTypeValidator)
        {
            _uiTypeValidator = uiTypeValidator;
            AnswerMaxLength = 4000;
        }

        public void ValidateDTO(AddTextFieldDTO dTO)
            => ValidateValues(dTO.AnswerLength, dTO.TextFieldUITypeId);

        public void ValidateDTO(UpdateTextFieldDTO dTO)
            => ValidateValues(dTO.AnswerLength, dTO.TextFieldUITypeId);

        private void ValidateValues(int? answerLength, int? textFieldUIType)
        {
            if (answerLength.HasValue && (answerLength < 1 || answerLength > AnswerMaxLength))
                throw new ApplicationValidationException($"valid AnswerLength is from 1 to {AnswerMaxLength}");

            if (textFieldUIType.HasValue)
                _uiTypeValidator.ThrowIfIdIsNotValid(textFieldUIType.Value);
        }
    }
}
