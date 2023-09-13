using OnlineExam.Application.Contract.DTOs.AnswerDTOs;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IDatabaseBasedAnswerValidator
    {
        void ValidateBeforeAdd(AddAnswerDTO dTO);
        void ValidateBeforeUpdate(UpdateAnswerDTO dTO);
    }
}