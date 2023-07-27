using OnlineExam.Application.Contract.DTOs.ExamDTOs;
using OnlineExam.Application.IMappers;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Mappers
{
    internal class ExamMapper : IExamMapper
    {
        public Exam? AddDTOToEntity(AddExamDTO? addExamDTO)
        {
            if (addExamDTO != null)
                return new()
                {
                    Title = addExamDTO.Title,
                    Start = addExamDTO.Start,
                    End = addExamDTO.End,
                    Published = addExamDTO.Published
                };
            return null;
        }

        public ShowExamDTO? EntityToShowDTO(Exam? entity)
        {
            if (entity != null)
                return new()
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Start = entity.Start,
                    End = entity.End,
                    CreatorUserId = entity.CreatorUserId,
                    Published = entity.Published
                };

            return null;
        }

        public void UpdateEntityByDTO(Exam old, UpdateExamDTO @new)
        {
            if (@new != null || old != null)
            {
                if (@new!.Title != null)
                    old.Title = @new.Title;

                if (@new.Start.HasValue)
                    old.Start = @new.Start.Value;

                if (@new.End.HasValue)
                    old.End = @new.End.Value;

                if (@new.Published.HasValue)
                    old.Published = @new.Published.Value;
            }
        }
    }
}
