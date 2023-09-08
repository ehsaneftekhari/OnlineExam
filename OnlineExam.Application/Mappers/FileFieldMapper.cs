using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Contract.DTOs.FileFieldDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Mappers
{
    internal class FileFieldMapper : IFileFieldMapper
    {
        public FileField? AddDTOToEntity(
            int questionId
            , AddFileFieldDTO? addFileFieldDTO
            , Func<IEnumerable<int>, IEnumerable<AllowedFileTypesField>> allowedFileTypesFieldExtractor)
        {
            if (addFileFieldDTO != null)
            {
                return new()
                {
                    QuestionId = questionId,
                    KiloByteMaximumSize = addFileFieldDTO.KiloByteMaximumSize,
                    AllowedFileTypes = allowedFileTypesFieldExtractor.Invoke(addFileFieldDTO.AllowedFileTypesIds).ToList()
                };
            }
            return null;
        }

        public ShowFileFieldDTO EntityToShowDTO(FileField fileField)
        {
            if (fileField != null)
            {

                Dictionary<int, string> allowedFileTypesNameIdPairs = new Dictionary<int, string>();
                foreach (var item in fileField.AllowedFileTypes)
                {
                    allowedFileTypesNameIdPairs.Add(item.Id, item.Name);
                }

                return new()
                {
                    Id = fileField.Id,
                    QuestionId = fileField.QuestionId,
                    KiloByteMaximumSize = fileField.KiloByteMaximumSize,
                    AllowedFileTypesNameIdPairs = allowedFileTypesNameIdPairs
                };
            }

            return null;
        }

        public void UpdateEntityByDTO(
            FileField old
            , UpdateFileFieldDTO @new
            , Func<IEnumerable<int>, IEnumerable<AllowedFileTypesField>> allowedFileTypesFieldExtractor)
        {
            if (@new != null || old != null)
            {
                if (@new!.KiloByteMaximumSize.HasValue)
                    old.KiloByteMaximumSize = (int)@new.KiloByteMaximumSize;

                if (@new.AllowedFileTypesIds != null && @new.AllowedFileTypesIds.Any())
                    old.AllowedFileTypes = allowedFileTypesFieldExtractor.Invoke(@new.AllowedFileTypesIds).ToList();
            }
        }
    }
}
