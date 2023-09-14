using OnlineExam.Application.Contract.DTOs.ExamDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.ExamServices
{
    public class ExamInternalService
    {
        readonly IExamRepository _examRepository;

        public ExamInternalService(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        internal Exam Add(Exam newExam)
        {
            ThrowIfExamIsNotValid(newExam);

            newExam!.CreatorUserId = "1";

            if (_examRepository.Add(newExam) > 0 && newExam.Id > 0)
                return newExam;

            throw new OEApplicationException();
        }

        internal void Delete(int examId)
        {
            var exam = GetById(examId);

            try
            {
                if (_examRepository.Delete(exam) < 0)
                    throw new OEApplicationException("Exam did not deleted");
            }
            catch
            {
                if (_examRepository.GetWithSectionsLoaded(examId).Sections.Any())
                    throw new OEApplicationException($"the exam has sections and can not be deleted");

                throw;
            }
        }

        internal Exam GetById(int examId)
        {
            ThrowIfExamIdIsNotValid(examId);

            var exam = _examRepository.GetById(examId);

            if (exam == null)
                throw new ApplicationSourceNotFoundException($"Exam with id:{examId} is not exists");

            return exam;
        }

        internal void ThrowExceptionIfExamIsNotExists(int examId) => GetById(examId);

        internal IEnumerable<Exam> GetAll(int skip, int take)
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

        internal void Update(Exam exam)
        {
            ThrowIfExamIsNotValid(exam);

            if (_examRepository.Update(exam) <= 0)
                throw new OEApplicationException("Exam did not updated");
        }

        internal void ThrowIfExamIdIsNotValid(int examId)
        {
            if (examId < 1)
                throw new ApplicationValidationException($"{nameof(examId)} can not be less than 1");
        }

        internal void ThrowIfExamIsNotValid(Exam exam)
        {
            if (exam == null)
                throw new ArgumentNullException();
        }
    }
}
