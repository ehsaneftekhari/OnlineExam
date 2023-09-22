using OnlineExam.Application.Contract.DTOs.AnswerDTOs;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IAnswerValidator
    {
        void ValidateDTO(AddAnswerDTO dTO);
        void ValidateDTO(UpdateAnswerDTO dTO);
    }
}