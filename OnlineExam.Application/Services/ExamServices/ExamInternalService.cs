using OnlineExam.Application.Abstractions.InternalService;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.ExamServices
{
    public class ExamInternalService : BaseInternalService<Exam, IExamRepository>
    {
        public ExamInternalService(IExamRepository examRepository) : base(examRepository) {}

        internal override Exam Add(Exam newExam)
        {
            ThrowIfEntityIsNull(newExam);

            if (_repository.Add(newExam) > 0 && newExam.Id > 0)
                return newExam;

            throw DidNotAddedException;
        }

        internal override void Delete(int examId)
        {
            var exam = GetById(examId);

            try
            {
                if (_repository.Delete(exam) < 0)
                    throw new OEApplicationException("Exam did not deleted");
            }
            catch
            {
                if (_repository.GetWithSectionsLoaded(examId).Sections.Any())
                    throw new OEApplicationException($"the exam has sections and can not be deleted");

                throw;
            }
        }
    }
}
