using OnlineExam.Application.Contract.DTOs.ExamUserDTOs;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IDatabaseBasedExamUserValidator
    {
        void DatabaseBasedValidateBeforeAdd(AddExamUserDTO dTO);
    }
}