using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IMappers
{
    public interface ICheckFieldOptionMapper
    {
        CheckFieldOption? AddDTOToEntity(int checkFieldId, AddCheckFieldOptionDTO? addCheckFieldOptionDTO);
        ShowCheckFieldOptionDTO EntityToShowDTO(CheckFieldOption addCheckFieldOptionDTO);
        void UpdateEntityByDTO(CheckFieldOption old, UpdateCheckFieldOptionDTO @new);
    }
}
