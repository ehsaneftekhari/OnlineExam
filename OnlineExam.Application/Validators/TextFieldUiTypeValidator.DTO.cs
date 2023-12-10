using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class TextFieldUiTypeDTOValidator : ITextFieldUiTypeDTOValidator
    {
        public void ThrowIfIdIsNotValid(int id)
        {
            if (!Enum.IsDefined(typeof(TextFieldUIType), id))
                throw new ApplicationValidationException("TextFieldUITypeId is not valid");
        }
    }
}
