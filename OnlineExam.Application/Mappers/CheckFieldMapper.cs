using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Mappers
{
    internal class CheckFieldMapper : ICheckFieldMapper
    {
        public CheckField? AddDTOToEntity(int questionId, AddCheckFieldDTO? addCheckFieldDTO)
        {
            if (addCheckFieldDTO != null)
            {
                return new()
                {
                    QuestionId = questionId,
                    CheckFieldUIType = (CheckFieldUIType)addCheckFieldDTO.CheckFieldUIType,
                    MaximumSelection = addCheckFieldDTO.MaximumSelection,
                    RandomizeOptions = addCheckFieldDTO.RandomizeOptions,
                };
            }
            return null;
        }

        public ShowCheckFieldDTO EntityToShowDTO(CheckField entity)
        {
            if (entity != null)
            {
                return new()
                {
                    Id = entity.Id,
                    CheckFieldUIType = (int)entity.CheckFieldUIType,
                    CheckFieldUITypeName = Enum.GetName(typeof(CheckFieldUIType), entity.CheckFieldUIType)!,
                    QuestionId = entity.QuestionId,
                    MaximumSelection = entity.MaximumSelection,
                    RandomizeOptions = entity.RandomizeOptions
                };
            }

            return null;
        }

        public void UpdateEntityByDTO(CheckField old, UpdateCheckFieldDTO @new)
        {
            if (@new != null || old != null)
            {
                if (@new!.RandomizeOptions.HasValue)
                    old.RandomizeOptions = (bool)@new.RandomizeOptions;

                if (@new!.MaximumSelection.HasValue)
                    old.MaximumSelection = (int)@new.MaximumSelection;

                if (@new!.CheckFieldUIType.HasValue)
                    old.CheckFieldUIType = (CheckFieldUIType)@new.CheckFieldUIType;
            }
        }
    }
}
