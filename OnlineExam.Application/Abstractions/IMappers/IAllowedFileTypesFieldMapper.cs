using OnlineExam.Application.Contract.DTOs.AllowedFileTypesFieldDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IMappers
{
    public interface IAllowedFileTypesFieldMapper
    {
        AllowedFileTypesField? AddDTOToEntity(AddAllowedFileTypesFieldDTO? addAllowedFileTypesFieldDTO);
        ShowAllowedFileTypesFieldDTO EntityToShowDTO(AllowedFileTypesField allowedFileTypesField);
        void UpdateEntityByDTO(AllowedFileTypesField old, UpdateAllowedFileTypesFieldDTO @new);
    }
}
