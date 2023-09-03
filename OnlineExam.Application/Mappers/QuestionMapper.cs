using OnlineExam.Application.Contract.DTOs.QuestionDTOs;
using OnlineExam.Application.IMappers;
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
                        ,addQuestionDTO.DurationHours
                        ,addQuestionDTO.DurationMinutes
                        ,addQuestionDTO.DurationSeconds),
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
                foreach(var property in typeof(UpdateQuestionDTO).GetProperties())
                {
                    var pName = property.Name;

                    var value = property.GetValue(@new);

                    if (value != null)
                    {
                        var entityProperty = typeof(Question)!.GetProperty(pName);
                        entityProperty.SetValue(old, value);
                    }
                    
                }
            }
        }
    }
}
