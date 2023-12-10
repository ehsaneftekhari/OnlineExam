using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.ExamServices
{
    public sealed class ExamInternalService 
        : BaseInternalService<Exam, IExamRepository>
        , IExamInternalService
    {
        public ExamInternalService(IExamRepository examRepository) : base(examRepository) { }

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
            Delete(exam);
        }

        internal override void Delete(Exam record)
        {
            try
            {
                if (_repository.Delete(record) < 0)
                    throw new OEApplicationException("Exam did not deleted");
            }
            catch
            {
                var fullDataExam = _repository.GetFullyLoaded(record.Id);

                if (fullDataExam.Sections.Any())
                    throw new OEApplicationException($"the exam has sections and can not be deleted");

                if (fullDataExam.ExamUsers.Any())
                    throw new OEApplicationException($"the exam has exam users and can not be deleted");

                throw;
            }
        }
    }
}
