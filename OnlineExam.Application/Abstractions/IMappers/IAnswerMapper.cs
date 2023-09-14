using OnlineExam.Application.Contract.DTOs.AnswerDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IMappers
{
    public interface IAnswerMapper
    {
        Answer? AddDTOToEntity(AddAnswerDTO? dTO, DateTime dateTime);
        ShowAnswerDTO? EntityToShowDTO(Answer? entity);
        void UpdateEntityByDTO(Answer old, UpdateAnswerDTO @new);
    }
}