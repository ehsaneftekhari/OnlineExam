using OnlineExam.Application.Contract.DTOs.TagDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.IMappers
{
    public interface ITagMapper
    {
        Tag? AddDTOToEntity(AddTagDTO? addExamDTO);
        ShowTagDTO? EntityToShowDTO(Tag? entity);
        void UpdateEntityByDTO(Tag old, UpdateTagDTO @new);
    }
}
