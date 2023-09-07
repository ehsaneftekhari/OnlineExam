using OnlineExam.Application.Contract.DTOs.FileFieldDTOs;

namespace OnlineExam.Application.Abstractions.IValidators
{
    internal interface IFileFieldValidator
    {
        void ValidateDTO(AddFileFieldDTO dTO);
        void ValidateDTO(UpdateFileFieldDTO dTO);
    }
}