using OnlineExam.Application.Contract.DTOs.AllowedFileTypesFieldDTOs;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IAllowedFileTypesFieldValidator
    {
        void ValidateDTO(AddAllowedFileTypesFieldDTO dTO);
        void ValidateDTO(UpdateAllowedFileTypesFieldDTO dTO);
    }
}