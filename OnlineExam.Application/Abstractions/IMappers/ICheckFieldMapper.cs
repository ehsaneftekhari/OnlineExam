using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IMappers
{
    public interface ICheckFieldMapper
    {
        CheckField? AddDTOToEntity(int questionId, AddCheckFieldDTO? addCheckFieldDTO);
        ShowCheckFieldDTO EntityToShowDTO(CheckField addCheckFieldDTO);
        void UpdateEntityByDTO(CheckField old, UpdateCheckFieldDTO @new);
    }
}
