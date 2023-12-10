using OnlineExam.Application.Contract.DTOs.FileFieldDTOs;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IFileFieldDTOValidator
    {
        void ValidateDTO(AddFileFieldDTO dTO);
        void ValidateDTO(UpdateFileFieldDTO dTO);
    }
}