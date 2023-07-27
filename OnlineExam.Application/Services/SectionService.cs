using OnlineExam.Application.Contract.DTOs.SectionDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.IMappers;
using OnlineExam.Infrastructure.Contract.IRepositories;

namespace OnlineExam.Application.Services
{
    public class SectionService : ISectionService
    {
        readonly IExamRepository _examRepository;
        readonly ISectionRepository _sectionRepository;
        readonly ISectionMapper _sectionMapper;

        public SectionService(ISectionRepository sectionRepository, ISectionMapper sectionMapper, IExamRepository examRepository)
        {
            this._sectionRepository = sectionRepository;
            this._sectionMapper = sectionMapper;
            this._examRepository = examRepository;
        }

        public bool Add(AddSectionDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            try
            {
                var newSection = _sectionMapper.AddDTOToEntity(dTO);
                return _sectionRepository.Add(newSection) == 1;
            }
            catch(Exception ex)
            {
                if(_examRepository.GetById(dTO.ExamId) == null)
                    return false;

                throw;
            }
        }

        public bool Delete(int id)
        {
            return _sectionRepository.DeleteById(id) == 1;
        }

        public ShowSectionDTO? GetById(int id)
        {
            return _sectionMapper.EntityToShowDTO(_sectionRepository.GetById(id));
        }

        public bool Update(UpdateSectionDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            var section = _sectionRepository.GetById(dTO.Id);

            var result = false;

            if (section != null)
            {
                _sectionMapper.UpdateEntityByDTO(section, dTO);
                result = _sectionRepository.Update(section) == 1;
            }

            return result;
        }
    }
}
