using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Contract.DTOs.QuestionDTOs;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Mappers
{
    internal class QuestionMapper : IQuestionMapper
    {
        private readonly ISectionMapper _sectionMapper;

        public QuestionMapper(ISectionMapper sectionMapper)
        {
            _sectionMapper = sectionMapper;
        }

        public Question? AddDTOToEntity(int sectionId, AddQuestionDTO? addQuestionDTO)
        {
            if (addQuestionDTO != null)
                return new()
                {
                    SectionId = sectionId,
                    Text = addQuestionDTO.Text,
                    Score = addQuestionDTO.Score,
                    Duration = new TimeSpan(
                        addQuestionDTO.DurationDays
                        , addQuestionDTO.DurationHours
                        , addQuestionDTO.DurationMinutes
                        , addQuestionDTO.DurationSeconds),
                    Order = addQuestionDTO.Order
                };
            return null;
        }

        public ShowQuestionDTO? EntityToShowDTO(Question? entity)
        {
            if (entity != null)
            {
                return new()
                {
                    Id = entity.Id,
                    SectionId = entity.SectionId,
                    Text = entity.Text,
                    ImageAddress = entity.ImageAddress,
                    Score = entity.Score,
                    Duration = entity.Duration,
                    Order = entity.Order
                };
            }

            return null;
        }

        public void UpdateEntityByDTO(Question old, UpdateQuestionDTO @new)
        {
            if (@new != null || old != null)
            {
                if (@new!.Text != null)
                    old.Text = @new.Text;

                if (@new.ImageAddress != null)
                    old.ImageAddress = @new.ImageAddress;

                if (@new.Score.HasValue)
                    old.Score = @new.Score.Value;

                if (@new.DurationDays.HasValue
                    || @new.DurationHours.HasValue
                    || @new.DurationMinutes.HasValue
                    || @new.DurationSeconds.HasValue)
                    old.Duration = new TimeSpan(@new.DurationDays ?? 0, @new.DurationHours ?? 0
                        , @new.DurationMinutes ?? 0, @new.DurationSeconds ?? 0);

                if (@new.Order.HasValue)
                    old.Order = @new.Order.Value;
            }
        }
    }
}
