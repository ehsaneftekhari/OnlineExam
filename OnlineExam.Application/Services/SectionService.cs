using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Contract.DTOs.QuestionDTOs;
using OnlineExam.Application.Contract.DTOs.SectionDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Mappers;
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

        public ShowSectionDTO Add(int examId, AddSectionDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            try
            {
                var newSection = _sectionMapper.AddDTOToEntity(examId, dTO)!;
                if (_sectionRepository.Add(newSection) > 0 && newSection.Id > 0)
                    return _sectionMapper.EntityToShowDTO(newSection)!;

                throw new Exception();
            }
            catch
            {
                if (_examRepository.GetById(examId) == null)
                    throw new OEApplicationException($"Exam with id:{examId} is not exists");

                throw;
            }
        }

        public IEnumerable<ShowSectionDTO> GetAllByExamId(int examId, int skip, int take)
        {
            if (examId < 1)
                throw new ApplicationValidationException("examId can not be less than 1");

            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var sections =
                _sectionRepository.GetIQueryable()
                .Where(q => q.ExamId == examId)
                .Skip(skip)
                .Take(take)
                .ToList()
                .Select(_sectionMapper.EntityToShowDTO);

            if (!sections.Any())
            {
                if (_examRepository.GetById(examId) == null)
                    throw new ApplicationSourceNotFoundException($"Exam with id:{examId} is not exists");

                throw new ApplicationSourceNotFoundException($"there is no Section within Exam (examId:{examId})");
            }

            return sections!;
        }

        public void Delete(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var section = _sectionRepository.GetById(id);

            if (section == null)
                throw new ApplicationSourceNotFoundException($"Section with id:{id} is not exists");

            if (_sectionRepository.Delete(section) < 0)
                throw new Exception();
        }

        public ShowSectionDTO? GetById(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var section = _sectionRepository.GetById(id);

            if (section == null)
                throw new ApplicationSourceNotFoundException($"Section with id:{id} is not exists");

            return _sectionMapper.EntityToShowDTO(section);
        }

        public void Update(int id, UpdateSectionDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            var section = _sectionRepository.GetById(id);

            if (section == null)
                throw new ApplicationSourceNotFoundException($"Section with id:{id} is not exists");

            _sectionMapper.UpdateEntityByDTO(section, dTO);

            if(_sectionRepository.Update(section) <= 0)
                throw new Exception();
        }
    }
}
