using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.FileFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;

namespace OnlineExam.Application.Validators
{
    internal class FileFieldDTOValidator : IFileFieldDTOValidator
    {
        public void ValidateDTO(AddFileFieldDTO dTO)
            => DatabaseBasedValidateValues(dTO.KiloByteMaximumSize);

        public void ValidateDTO(UpdateFileFieldDTO dTO)
            => DatabaseBasedValidateValues(dTO.KiloByteMaximumSize);

        private void DatabaseBasedValidateValues(int? kiloByteMaximumSize)
        {
            if (kiloByteMaximumSize.HasValue && kiloByteMaximumSize < 1)
                throw new ApplicationValidationException("kiloByteMaximumSize can not be less then 1");

            if (kiloByteMaximumSize.HasValue && kiloByteMaximumSize > 100000)
                throw new ApplicationValidationException("kiloByteMaximumSize can not be more then 100000");
        }
    }
}
