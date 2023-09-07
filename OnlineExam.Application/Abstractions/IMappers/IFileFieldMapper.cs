using OnlineExam.Application.Contract.DTOs.FileFieldDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IMappers
{
    public interface IFileFieldMapper
    {
        FileField? AddDTOToEntity(int questionId, AddFileFieldDTO? addFileFieldDTO, Func<ICollection<int>, ICollection<AllowedFileTypesField>> allowedFileTypesFieldExtractor);
        ShowFileFieldDTO EntityToShowDTO(FileField fileField);
        void UpdateEntityByDTO(FileField old, UpdateFileFieldDTO @new, Func<ICollection<int>, ICollection<AllowedFileTypesField>> allowedFileTypesFieldExtractor);
    }
}
