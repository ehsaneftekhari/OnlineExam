using OnlineExam.Application.Contract.DTOs.AnswerDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IAnswerDTOValidator
    {
        void ValidateDTO(AddAnswerDTO dTO);
        void ValidateDTO(UpdateAnswerDTO dTO);
        void ThrowIfExamUsersAreNotValid(Exam parenExam);
        void ThrowIfExamIsNotActive(DateTime now, Exam exam);
    }
}