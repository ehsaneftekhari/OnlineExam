using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Contract.DTOs.AnswerDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Mappers
{
    internal class AnswerMapper : IAnswerMapper
    {
        public Answer? AddDTOToEntity(AddAnswerDTO? dTO, int ExamUserId, DateTime dateTime)
        {
            if (dTO != null)
                return new()
                {
                    ExamUserId = ExamUserId,
                    QuestionId = dTO.QuestionId,
                    Content = dTO.Content,
                    DateTime = dateTime
                };
            return null;
        }

        public ShowAnswerDTO? EntityToShowDTO(Answer? entity)
        {
            if (entity != null)
            {
                return new()
                {
                    Id = entity.Id,
                    ExamUserId = entity.ExamUserId,
                    QuestionId = entity.QuestionId,
                    Content = entity.Content,
                    DateTime = entity.DateTime,
                    EarnedScore = entity.EarnedScore
                };
            }

            return null;
        }

        public void UpdateEntityByDTO(Answer old, UpdateAnswerDTO @new)
        {
            if (@new != null || old != null)
            {
                old.EarnedScore = @new.EarnedScore;
            }
        }
    }
}
