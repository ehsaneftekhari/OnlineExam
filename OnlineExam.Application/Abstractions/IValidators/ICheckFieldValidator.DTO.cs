using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface ICheckFieldDTOValidator
    {
        void ValidateDTO(AddCheckFieldDTO dTO);
        void ValidateDTO(UpdateCheckFieldDTO dTO);
    }
}