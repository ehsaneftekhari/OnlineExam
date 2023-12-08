using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface ICheckFieldOptionRelationValidator
    {
        void DatabaseBasedValidate(int checkFieldId, AddCheckFieldOptionDTO dTO);
        void DatabaseBasedValidate(int checkFieldId, int checkFieldOptionId, UpdateCheckFieldOptionDTO dTO);
    }
}