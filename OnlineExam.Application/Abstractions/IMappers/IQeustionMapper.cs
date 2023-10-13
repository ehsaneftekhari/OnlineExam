using OnlineExam.Application.Contract.DTOs.QuestionDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IMappers
{
    public interface IQuestionMapper
    {
        Question? AddDTOToEntity(int sectionId, AddQuestionDTO? addQuestionDTO);
        ShowQuestionDTO? EntityToShowDTO(Question? entity);
        void UpdateEntityByDTO(Question old, UpdateQuestionDTO @new);
    }
}
