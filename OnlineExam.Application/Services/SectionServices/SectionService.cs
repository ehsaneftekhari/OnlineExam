using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.SectionDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Services.ExamUserServices;
using OnlineExam.Application.Validators;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.SectionServices
{
    public sealed class SectionService : ISectionService
    {
        readonly ISectionInternalService _sectionInternalService;
        readonly ISectionMapper _sectionMapper;
        readonly ISectionValidator _sectionValidator;

        public SectionService(ISectionInternalService sectionInternalService,
                              ISectionMapper sectionMapper,
                              ISectionValidator sectionValidator)
        {
            _sectionInternalService = sectionInternalService;
            _sectionMapper = sectionMapper;
            _sectionValidator = sectionValidator;
        }

        public ShowSectionDTO Add(int examId, AddSectionDTO Section)
        {
            _sectionValidator.ThrowIfUserIsNotExamCreator(Section.CreatorUserId, examId);

            var newSection = _sectionMapper.AddDTOToEntity(examId, Section)!;

            _sectionInternalService.Add(newSection);

            return _sectionMapper.EntityToShowDTO(newSection)!;
        }

        public IEnumerable<ShowSectionDTO> GetAllByExamId(int examId, string issuerUserId, int skip, int take)
        {
            _sectionValidator.ThrowIfUserIsNotExamCreator(issuerUserId, examId);

            return _sectionInternalService.GetAllByParentId(examId, skip, take).Select(_sectionMapper.EntityToShowDTO)!;
        }

        public void Delete(int id, string issuerUserId)
        {
            var section = GetSectionHasExamIncluded(id);

            _sectionValidator.ThrowIfUserIsNotExamCreator(issuerUserId, section.Exam);

            _sectionInternalService.Delete(section);
        }

        public ShowSectionDTO? GetById(int id, string issuerUserId)
        {
            var section = GetSectionHasExamThenExamUsersIncluded(id);

            _sectionValidator.ThrowIfUserIsNotExamCreatorOrExamUserCreator(issuerUserId, section);

            return _sectionMapper.EntityToShowDTO(section);
        }

        public void Update(int id, string issuerUserId, UpdateSectionDTO dTO)
        {
            var section = GetSectionHasExamIncluded(id);

            _sectionValidator.ThrowIfUserIsNotExamCreatorOrExamUserCreator(issuerUserId, section);

            _sectionMapper.UpdateEntityByDTO(section, dTO);

            _sectionInternalService.Update(section);
        }


        private Section GetSectionHasExamThenExamUsersIncluded(int id)
        {
            return _sectionInternalService.GetById(id,
                _sectionInternalService.GetIQueryable()
                    .Include(x => x.Exam)
                    .ThenInclude(x => x.ExamUsers));
        }
        
        private Section GetSectionHasExamIncluded(int id)
        {
            return _sectionInternalService.GetById(id,
                _sectionInternalService.GetIQueryable()
                    .Include(x => x.Exam));
        }
    }
}
