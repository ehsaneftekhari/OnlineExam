using OnlineExam.Application.Contract.DTOs.FileFieldDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IMappers
{
    public interface IFileFieldMapper
    {
        FileField? AddDTOToEntity(int questionId, AddFileFieldDTO? addFileFieldDTO, Func<IEnumerable<int>, IEnumerable<AllowedFileTypesField>> allowedFileTypesFieldExtractor);
        ShowFileFieldDTO EntityToShowDTO(FileField fileField);
        void UpdateEntityByDTO(FileField old, UpdateFileFieldDTO @new, Func<IEnumerable<int>, IEnumerable<AllowedFileTypesField>> allowedFileTypesFieldExtractor);
    }
}
