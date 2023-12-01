using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface ICheckFieldOptionValidator
    {
        void ValidateDTO(AddCheckFieldOptionDTO dTO);
        void ValidateDTO(UpdateCheckFieldOptionDTO dTO);
        void ThrowIfUserIsNotExamCreator(int checkFieldId, string userId);
        void ThrowIfUserIsNotExamCreatorOrExamUser(int checkFieldId, string userId);
        void ThrowIfUserIsNotExamCreatorOrExamUser(string userId, Exam exam);
        void ThrowIfUserIsNotExamCreator(string userId, Exam exam);
    }
}