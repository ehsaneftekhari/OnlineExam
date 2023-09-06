using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class CheckFieldValidator : ICheckFieldValidator
    {
        public void ValidateDTO(AddCheckFieldDTO dTO)
            => ValidateValues(dTO.MaximumSelection, dTO.CheckFieldUIType);

        public void ValidateDTO(UpdateCheckFieldDTO dTO)
            => ValidateValues(dTO.MaximumSelection, dTO.CheckFieldUIType);

        private void ValidateValues(int? maximumSelection, int? checkFieldUIType)
        {
            if (maximumSelection.HasValue && maximumSelection < 1)
                throw new ApplicationValidationException("maximumSelection can not be less then 1");

            if (checkFieldUIType.HasValue && !Enum.IsDefined(typeof(CheckFieldUIType), checkFieldUIType))
                throw new ApplicationValidationException("checkFieldUIType is not valid");
        }
    }
}
