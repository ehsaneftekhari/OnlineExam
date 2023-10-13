using OnlineExam.Application.Contract.DTOs.SectionDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IMappers
{
    public interface ISectionMapper
    {
        Section? AddDTOToEntity(int examId, AddSectionDTO? addSectionDTO);
        ShowSectionDTO? EntityToShowDTO(Section? entity);
        void UpdateEntityByDTO(Section old, UpdateSectionDTO @new);
    }
}
