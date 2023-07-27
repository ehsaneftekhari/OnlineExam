using OnlineExam.Application.Contract.DTOs.SectionDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.IMappers
{
    public interface ISectionMapper
    {
        Section? AddDTOToEntity(AddSectionDTO? addSectionDTO);
        ShowSectionDTO? EntityToShowDTO(Section? entity);
        void UpdateEntityByDTO(Section old, UpdateSectionDTO @new);
    }
}
