using OnlineExam.Application.Contract.DTOs.ExamUserDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IMappers
{
    public interface IExamUserMapper
    {
        ExamUser? AddDTOToEntity(AddExamUserDTO? dTO);
        ShowExamUserDTO? EntityToShowDTO(ExamUser? entity);
    }
}