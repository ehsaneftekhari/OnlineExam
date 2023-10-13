using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface ICheckFieldOptionValidator
    {
        void ValidateDTO(AddCheckFieldOptionDTO dTO);
        void ValidateDTO(UpdateCheckFieldOptionDTO dTO);
    }
}