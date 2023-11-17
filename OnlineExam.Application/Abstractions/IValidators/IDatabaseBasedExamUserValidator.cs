using OnlineExam.Application.Contract.DTOs.ExamUserDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IDatabaseBasedExamUserValidator
    {
        void ValidateBeforeAdd(AddExamUserDTO dTO);
        void ValidateIfExamUserCanFinish(string issuerUserId, ExamUser examUser);
        void ThrowIfUserIsNotCreatorOfExamUserOrExam(string issuerUserId, ExamUser examUser);
        void ThrowIfUserIsNotCreatorOfExamUser(string issuerUserId, ExamUser examUser);
        void ThrowIfUserIsNotCreatorOfExam(int examId, string issuerUserId);
    }
}