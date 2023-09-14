using OnlineExam.Application.Contract.DTOs.ExamDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services
{
    public class ExamInternalService
    {
        readonly IExamRepository _examRepository;

        public ExamInternalService(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public Exam Add(Exam newExam)
        {
            if (newExam == null)
                throw new ArgumentNullException();

            newExam!.CreatorUserId = "1";

            if (_examRepository.Add(newExam) > 0 && newExam.Id > 0)
                return newExam;

            throw new OEApplicationException();
        }

        public void Delete(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var exam = _examRepository.GetById(id);

            if (exam == null)
                throw new ApplicationSourceNotFoundException($"Exam with id:{id} is not exists");

            try
            {
                if (_examRepository.Delete(exam) < 0)
                    throw new OEApplicationException("Exam did not deleted");
            }
            catch
            {
                if (_examRepository.GetWithSectionsLoaded(id).Sections.Any())
                    throw new OEApplicationException($"the exam has sections and can not be deleted");

                throw;
            }
        }

        public Exam GetById(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var exam = _examRepository.GetById(id);

            if (exam == null)
                throw new ApplicationSourceNotFoundException($"Exam with id:{id} is not exists");

            return exam;
        }

        public IEnumerable<Exam> GetAll(int skip, int take)
        {
            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var exams =
                _examRepository.GetIQueryable()
                .Skip(skip)
                .Take(take)
                .ToList();

            if (!exams.Any())
                throw new ApplicationSourceNotFoundException($"there is no Exam");

            return exams!;
        }

        public void Update(int id, Exam exam)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            if (exam == null)
                throw new ArgumentNullException();

            if (_examRepository.Update(exam) <= 0)
                throw new Exception();
        }
    }
}
