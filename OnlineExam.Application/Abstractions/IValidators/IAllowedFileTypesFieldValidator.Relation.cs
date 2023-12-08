using OnlineExam.Application.Contract.DTOs.AllowedFileTypesFieldDTOs;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IAllowedFileTypesFieldRelationValidator
    {
        void Validate(AddAllowedFileTypesFieldDTO dTO);
        void Validate(int allowedFileTypesField, UpdateAllowedFileTypesFieldDTO dTO);
    }
}