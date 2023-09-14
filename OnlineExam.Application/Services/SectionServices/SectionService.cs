using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Contract.DTOs.SectionDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;

namespace OnlineExam.Application.Services.SectionServices
{
    public class SectionService : ISectionService
    {
        readonly SectionInternalService _sectionInternalService;
        readonly ISectionMapper _sectionMapper;

        public SectionService(SectionInternalService sectionInternalService,
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
         => _sectionInternalService.GetAllByExamId(examId, skip, take).Select(_sectionMapper.EntityToShowDTO)!;

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
