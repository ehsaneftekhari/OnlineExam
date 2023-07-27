using OnlineExam.Application.Contract.DTOs.TagDTOs;
using OnlineExam.Application.IMappers;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Mappers
{
    internal class TagMapper : ITagMapper
    {
        public Tag? AddDTOToEntity(AddTagDTO? addExamDTO)
        {
            if (addExamDTO != null)
                return new()
                {
                    Name = addExamDTO.Name,
                    Description = addExamDTO.Description,
                };
            return null;
        }

        public ShowTagDTO? EntityToShowDTO(Tag? entity)
        {
            if (entity != null)
            {
                return new()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description
                };
            }

            return null;
        }

        public void UpdateEntityByDTO(Tag old, UpdateTagDTO @new)
        {
            if (@new != null || old != null)
            {
                if (@new!.Name != null)
                    old.Name = @new.Name;

                if (@new.Description != null)
                    old.Description = @new.Description;
            }
        }
    }
}
