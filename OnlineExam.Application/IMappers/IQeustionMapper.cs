using OnlineExam.Application.Contract.DTOs.QuestionDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.IMappers
{
    public interface IQuestionMapper
    {
        Question? AddDTOToEntity(int sectionId, AddQuestionDTO? addQuestionDTO);
        ShowQuestionDTO? EntityToShowDTO(Question? entity);
        void UpdateEntityByDTO(Question old, UpdateQuestionDTO @new);
    }
}
