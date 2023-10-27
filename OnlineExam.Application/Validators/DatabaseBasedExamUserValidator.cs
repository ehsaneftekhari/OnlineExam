using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.ExamUserDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Services.ExamServices;

namespace OnlineExam.Application.Validators
{
    public class DatabaseBasedExamUserValidator : IDatabaseBasedExamUserValidator
    {
        readonly ExamInternalService _examInternalService;

        public DatabaseBasedExamUserValidator(ExamInternalService examRepository)
        {
            _examInternalService = examRepository;
        }

        public void DatabaseBasedValidate(AddExamUserDTO dTO)
            => DatabaseBasedValidateValues(dTO.ExamId);

        private void DatabaseBasedValidateValues(int examId)
        {
            var exam = _examInternalService.GetById(examId);

            if (exam == null)
                throw new ApplicationValidationException($"there is no Exam with id: {examId}");

            if (!exam.Published)
                throw new ApplicationValidationException($"Exam (id: {examId}) is not published");

            if (exam.End < DateTime.Now)
                throw new ApplicationValidationException($"the Exam (id: {examId}) is finished at {exam.End}");
        }
    }
}
