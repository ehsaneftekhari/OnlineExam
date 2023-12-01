using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class ExamUserActionValidator : IExamUserActionValidator
    {
        readonly IExamInternalService _examInternalService;

        public ExamUserActionValidator(IExamInternalService examInternalService)
        {
            _examInternalService = examInternalService;
        }

        public void ValidateIfExamUserCanFinish(ExamUser examUser)
        {
            if (examUser.End != null)
                throw new ApplicationValidationException($"The Exam ({examUser.ExamId}) has finished for User (id : {examUser.UserId}) at {examUser.End}");

            var exam = _examInternalService.GetById(examUser.ExamId);

            if (exam.End < DateTime.Now)
                throw new ApplicationValidationException($"The Exam ({examUser.ExamId}) is already finished");
        }
    }
}
