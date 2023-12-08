using OnlineExam.Application.Contract.DTOs.ExamUserDTOs;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IExamUserDTOValidator
    {
        void ValidateDTO(AddExamUserDTO dTO);
    }
}