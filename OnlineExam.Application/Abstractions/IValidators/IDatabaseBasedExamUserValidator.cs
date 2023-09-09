using OnlineExam.Application.Contract.DTOs.ExamUserDTOs;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IDatabaseBasedExamUserValidator
    {
        void DatabaseBasedValidate(AddExamUserDTO dTO);
    }
}