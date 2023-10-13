using OnlineExam.Application.Contract.DTOs.AllowedFileTypesFieldDTOs;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IDatabaseBasedAllowedFileTypesFieldValidator
    {
        void DatabaseBasedValidate(AddAllowedFileTypesFieldDTO dTO);
        void DatabaseBasedValidate(int allowedFileTypesField, UpdateAllowedFileTypesFieldDTO dTO);
    }
}