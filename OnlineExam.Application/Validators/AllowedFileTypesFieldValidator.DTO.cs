using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.AllowedFileTypesFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;

namespace OnlineExam.Application.Validators
{
    internal class AllowedFileTypesFieldDTOValidator : IAllowedFileTypesFieldDTOValidator
    {
        public void ValidateDTO(AddAllowedFileTypesFieldDTO dTO)
        => DatabaseBasedValidateValues(dTO.Name, dTO.Extension);

        public void ValidateDTO(UpdateAllowedFileTypesFieldDTO dTO)
            => DatabaseBasedValidateValues(dTO.Name, dTO.Extension);

        private void DatabaseBasedValidateValues(string? name, string? extension)
        {
            if (name != null && name.Length > 512)
                throw new ApplicationValidationException("Name can not be longer than 512 characters");

            if (extension != null && extension.Length > 8)
                throw new ApplicationValidationException("extension can not be longer than 8 characters");
        }
    }
}
