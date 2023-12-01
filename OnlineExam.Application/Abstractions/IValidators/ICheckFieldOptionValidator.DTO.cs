using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface ICheckFieldOptionDTOValidator
    {
        void ValidateDTO(AddCheckFieldOptionDTO dTO);
        void ValidateDTO(UpdateCheckFieldOptionDTO dTO);
    }
}