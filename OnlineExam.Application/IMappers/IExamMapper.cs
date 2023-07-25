using OnlineExam.Application.Contract.DTOs.ExamDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.IMappers
{
    public interface IExamMapper
    {
        Exam? AddDTOToEntity(AddExamDTO? addExamDTO);
        ShowExamDTO? EntityToShowDTO(Exam? entity);
        void UpdateEntityByDTO(Exam old, UpdateExamDTO @new);
    }
}
