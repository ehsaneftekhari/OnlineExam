using OnlineExam.Application.Contract.DTOs.AllowedFileTypesFieldDTOs;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IAllowedFileTypesFieldDTOValidator
    {
        void ValidateDTO(AddAllowedFileTypesFieldDTO dTO);
        void ValidateDTO(UpdateAllowedFileTypesFieldDTO dTO);
    }
}