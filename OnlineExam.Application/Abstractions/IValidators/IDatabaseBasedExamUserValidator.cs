using OnlineExam.Application.Contract.DTOs.ExamUserDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IDatabaseBasedExamUserValidator
    {
        void DatabaseBasedValidateBeforeAdd(AddExamUserDTO dTO);
        void ValidateIfExamUserCanFinish(string issuerUserId, ExamUser examUser);
        void ThrowIfUserIsNotExamUserOwnerOrExamOwner(string issuerUserId, ExamUser examUser);
        void ThrowIfUserIsNotOwner(int examId, string issuerUserId);
    }
}