using OnlineExam.Application.Contract.DTOs.FileFieldDTOs;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IFileFieldValidator
    {
        void ValidateDTO(AddFileFieldDTO dTO);
        void ValidateDTO(UpdateFileFieldDTO dTO);
    }
}