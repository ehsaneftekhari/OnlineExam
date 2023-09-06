using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Mappers
{
    internal class CheckFieldOptionMapper : ICheckFieldOptionMapper
    {
        public CheckFieldOption? AddDTOToEntity(int questionId, AddCheckFieldOptionDTO? addCheckFieldOptionDTO)
        {
            if (addCheckFieldOptionDTO != null)
            {
                return new()
                {
                    Order = addCheckFieldOptionDTO.Order,
                    Score = addCheckFieldOptionDTO.Score,
                    Text = addCheckFieldOptionDTO.Text
                };
            }
            return null;
        }

        public ShowCheckFieldOptionDTO EntityToShowDTO(CheckFieldOption addCheckFieldOption)
        {
            if (addCheckFieldOption != null)
            {
                return new()
                {
                    Id = addCheckFieldOption.Id,
                    Order = addCheckFieldOption.Order,
                    CheckFieldId = addCheckFieldOption.CheckFieldId,
                    Score = addCheckFieldOption.Score,
                    ImageAddress = addCheckFieldOption.ImageAddress,
                    Text = addCheckFieldOption.Text
                };
            }

            return null;
        }

        public void UpdateEntityByDTO(CheckFieldOption old, UpdateCheckFieldOptionDTO @new)
        {
            if (@new != null || old != null)
            {
                if (@new!.Order.HasValue)
                    old.Order = @new.Order.Value;

                if (@new.Score.HasValue)
                    old.Score = @new.Score.Value;

                if (old.Text != null)
                    old.Text = @new.Text;
            }
        }
    }
}
