﻿using OnlineExam.Application.Contract.DTOs.SectionDTOs;
using OnlineExam.Application.IMappers;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Mappers
{
    internal class SectionMapper : ISectionMapper
    {
        private readonly IExamMapper _examMapper;

        public SectionMapper(IExamMapper examMapper)
        {
            _examMapper = examMapper;
        }

        public Section? AddDTOToEntity(AddSectionDTO? addSectionDTO)
        {
            if (addSectionDTO != null)
                return new()
                {
                    Title = addSectionDTO.Title,
                    Order = addSectionDTO.Order,
                    RandomizeQuestions = addSectionDTO.RandomizeQuestions,
                    ExamId = addSectionDTO.ExamId
                };
            return null;
        }

        public ShowSectionDTO? EntityToShowDTO(Section? entity)
        {
            if (entity != null)
            {
                return new()
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Order = entity.Order,
                    RandomizeQuestions = entity.RandomizeQuestions,
                    Exam = entity.Exam != null ? _examMapper.EntityToShowDTO(entity.Exam)! : null,
                };
            }

            return null;
        }

        public void UpdateEntityByDTO(Section old, UpdateSectionDTO @new)
        {
            if (@new != null || old != null)
            {
                if (@new!.Title != null)
                    old.Title = @new.Title;

                if (@new.Order.HasValue)
                    old.Order = @new.Order.Value;

                if (@new.RandomizeQuestions.HasValue)
                    old.RandomizeQuestions = @new.RandomizeQuestions.Value;
            }
        }
    }
}
