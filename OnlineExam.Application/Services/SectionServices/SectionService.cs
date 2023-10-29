﻿using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.InternalService;
using OnlineExam.Application.Contract.DTOs.SectionDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.SectionServices
{
    public sealed class SectionService : ISectionService
    {
        readonly IBaseInternalService<Section, Exam> _sectionInternalService;
        readonly ISectionMapper _sectionMapper;

        public SectionService(IBaseInternalService<Section, Exam> sectionInternalService,
                              ISectionMapper sectionMapper)
        {
            _sectionInternalService = sectionInternalService;
            _sectionMapper = sectionMapper;
        }

        public ShowSectionDTO Add(int examId, AddSectionDTO Section)
        {
            var newSection = _sectionMapper.AddDTOToEntity(examId, Section)!;
            _sectionInternalService.Add(newSection);
            return _sectionMapper.EntityToShowDTO(newSection)!;
        }

        public IEnumerable<ShowSectionDTO> GetAllByExamId(int examId, int skip, int take)
         => _sectionInternalService.GetAllByParentId(examId, skip, take).Select(_sectionMapper.EntityToShowDTO)!;

        public void Delete(int sectionId) => _sectionInternalService.Delete(sectionId);

        public ShowSectionDTO? GetById(int sectionId)
            => _sectionMapper.EntityToShowDTO(_sectionInternalService.GetById(sectionId));

        public void Update(int sectionId, UpdateSectionDTO dTO)
        {
            var section = _sectionInternalService.GetById(sectionId);

            _sectionMapper.UpdateEntityByDTO(section, dTO);

            _sectionInternalService.Update(section);
        }
    }
}
