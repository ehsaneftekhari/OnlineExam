using OnlineExam.Application.Contract.DTOs.SectionDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Services.ExamServices;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.SectionServices
{
    public class SectionInternalService
    {
        readonly ExamInternalService _examInternalService;
        readonly ISectionRepository _sectionRepository;

        public SectionInternalService(ExamInternalService examInternalService, ISectionRepository sectionRepository)
        {
            _examInternalService = examInternalService;
            _sectionRepository = sectionRepository;
        }

        internal Section Add(int examId, Section newSection)
        {
            _examInternalService.ThrowIfExamIdIsNotValid(examId);

            ThrowIfSectionIsNotValid(newSection);

            try
            {
                if (_sectionRepository.Add(newSection) > 0 && newSection.Id > 0)
                    return newSection;

                throw new OEApplicationException();
            }
            catch
            {
                _examInternalService.ThrowExceptionIfExamIsNotExists(examId);

                throw;
            }
        }

        internal IEnumerable<Section> GetAllByExamId(int examId, int skip, int take)
        {
            _examInternalService.ThrowIfExamIdIsNotValid(examId);

            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var sections =
                _sectionRepository.GetIQueryable()
                .Where(q => q.ExamId == examId)
                .Skip(skip)
                .Take(take)
                .ToList();

            if (!sections.Any())
            {
                _examInternalService.ThrowExceptionIfExamIsNotExists(examId);

                throw new ApplicationSourceNotFoundException($"there is no Section within Exam (examId:{examId})");
            }

            return sections!;
        }

        internal Section GetById(int sectionId)
        {
            ThrowIfSectionIdIsNotValid(sectionId);

            var section = _sectionRepository.GetById(sectionId);

            if (section == null)
                throw new ApplicationSourceNotFoundException($"Section with id:{sectionId} is not exists");

            return section;
        }

        internal void ThrowExceptionIfSectionIsNotExists(int sectionId) => GetById(sectionId);

        internal void Delete(int sectionId)
        {
            var section = GetById(sectionId);

            if (_sectionRepository.Delete(section) < 0)
                throw new OEApplicationException("Section did not deleted");
        }

        internal void Update(Section section)
        {
            ThrowIfSectionIsNotValid(section);

            if (_sectionRepository.Update(section) <= 0)
                throw new OEApplicationException("Section did not updated");
        }

        internal void ThrowIfSectionIdIsNotValid(int sectionId)
        {
            if (sectionId < 1)
                throw new ApplicationValidationException($"{nameof(sectionId)} can not be less than 1");
        }

        internal void ThrowIfSectionIsNotValid(Section section)
        {
            if (section == null)
                throw new ArgumentNullException();
        }
    }
}
