using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Contract.DTOs.AllowedFileTypesFieldDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Mappers
{
    internal class AllowedFileTypesFieldMapper : IAllowedFileTypesFieldMapper
    {
        public AllowedFileTypesField? AddDTOToEntity(AddAllowedFileTypesFieldDTO? addAllowedFileTypesFieldDTO)
        {
            if (addAllowedFileTypesFieldDTO != null)
            {
                return new()
                {
                    Name = addAllowedFileTypesFieldDTO.Name,
                    Extension = addAllowedFileTypesFieldDTO.Extension
                };
            }
            return null;
        }

        public ShowAllowedFileTypesFieldDTO EntityToShowDTO(AllowedFileTypesField allowedFileTypesField)
        {
            if (allowedFileTypesField != null)
            {
                return new()
                {
                    Id = allowedFileTypesField.Id,
                    Name = allowedFileTypesField.Name,
                    Extension = allowedFileTypesField.Extension
                };
            }

            return null;
        }

        public void UpdateEntityByDTO(AllowedFileTypesField old, UpdateAllowedFileTypesFieldDTO @new)
        {
            if (@new != null || old != null)
            {
                if (@new!.Name != null)
                    old.Name = @new.Name;

                if (@new.Extension != null)
                    old.Extension = @new.Extension;
            }
        }
    }
}
