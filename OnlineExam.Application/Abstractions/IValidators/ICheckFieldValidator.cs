using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface ICheckFieldValidator
    {
        void ValidateDTO(AddCheckFieldDTO dTO);
        void ValidateDTO(UpdateCheckFieldDTO dTO);
        void ThrowIfUserIsNotExamCreator(int questionId, string issuerUserId);
        void ThrowIfUserIsNotExamCreatorOrExamUser(int questionId, string issuerUserId);
    }
}