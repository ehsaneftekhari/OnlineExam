using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Contract.DTOs.ExamDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;

using OnlineExam.Application.Mappers;
using OnlineExam.Infrastructure.Contract.IRepositories;

namespace OnlineExam.Application.Services
{
    public class ExamService : IExamService
    {
        readonly IExamRepository _examRepository;
        readonly IExamMapper _examMapper;

        public ExamService(IExamRepository examRepository, IExamMapper examMapper)
        {
            this._examRepository = examRepository;
            this._examMapper = examMapper;
        }

        public ShowExamDTO Add(AddExamDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            var newExam = _examMapper.AddDTOToEntity(dTO);
            newExam!.CreatorUserId = "1";

            if (_examRepository.Add(newExam) > 0 && newExam.Id > 0)
                return _examMapper.EntityToShowDTO(newExam)!;

            throw new Exception();
        }

        public void Delete(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var exam = _examRepository.GetWithSectionsLoaded(id);

            if (exam == null)
                throw new ApplicationSourceNotFoundException($"Exam with id:{id} is not exists");

            try
            {
                if (_examRepository.Delete(exam) < 0)
                    throw new Exception();
            }
            catch
            {
                if (_examRepository.GetWithSectionsLoaded(id).Sections.Any())
                    throw new OEApplicationException($"the exam has sections and can not be deleted");

                throw;
            }
        }

        public ShowExamDTO? GetById(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var exam = _examRepository.GetById(id);

            if (exam == null)
                throw new ApplicationSourceNotFoundException($"Exam with id:{id} is not exists");

            return _examMapper.EntityToShowDTO(exam);
        }

        public IEnumerable<ShowExamDTO> GetAll(int skip, int take)
        {
            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var exams =
                _examRepository.GetIQueryable()
                .Skip(skip)
                .Take(take)
                .ToList()
                .Select(_examMapper.EntityToShowDTO);

            if (!exams.Any())
                throw new ApplicationSourceNotFoundException($"there is no Exam");

            return exams!;
        }

        public void Update(int id, UpdateExamDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            var exam = _examRepository.GetById(id);

            if (exam == null)
                throw new ApplicationSourceNotFoundException($"Exam with id:{id} is not exists");

            _examMapper.UpdateEntityByDTO(exam, dTO);

            if (_examRepository.Update(exam) <= 0)
                throw new Exception();
        }
    }
}
