using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;

namespace OnlineExam.Application.Validators
{
    public class CheckFieldOptionValidator : ICheckFieldOptionValidator
    {
        public void ValidateDTO(AddCheckFieldOptionDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            ValidateValues(dTO.Order, dTO.Text);
        }

        public void ValidateDTO(UpdateCheckFieldOptionDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            ValidateValues(dTO.Order, dTO.Text);
        }

        private void ValidateValues(int? Order, string? Text)
        {
            if (Order.HasValue && Order < 1)
                throw new ApplicationValidationException("Order can not be less then 1");

            if (Text != null && Text!.Length > 4000)
                throw new ApplicationValidationException("Text length can not be more than 4000 characters");
        }
    }
}
