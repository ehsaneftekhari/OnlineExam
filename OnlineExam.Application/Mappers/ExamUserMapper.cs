using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Contract.DTOs.ExamUserDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Mappers
{
    internal class ExamUserMapper : IExamUserMapper
    {
        public ExamUser? AddDTOToEntity(AddExamUserDTO? dTO, DateTime Start)
        {
            if (dTO != null)
                return new()
                {
                    UserId = dTO.UserId,
                    ExamId = dTO.ExamId,
                    Start = Start
                };
            return null;
        }

        public ShowExamUserDTO? EntityToShowDTO(ExamUser? entity)
        {
            if (entity != null)
            {
                return new()
                {
                    Id = entity.Id,
                    UserId = entity.UserId,
                    ExamId = entity.ExamId,
                    Start = entity.Start,
                    End = entity.End,
                    EarnedScore = entity.EarnedScore,
                };
            }

            return null;
        }
    }
}
