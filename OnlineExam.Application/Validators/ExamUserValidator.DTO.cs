using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.ExamUserDTOs;
using OnlineExam.Application.Contract.Exceptions;

namespace OnlineExam.Application.Validators
{
    public class ExamUserDTOValidator : IExamUserDTOValidator
    {

        readonly IExamInternalService _examInternalService;

        readonly IExamUserInternalService _examUserInternalService;

        public ExamUserDTOValidator(IExamInternalService examInternalService,
                                 IExamUserInternalService examUserInternalService)
        {
            _examInternalService = examInternalService;
            _examUserInternalService = examUserInternalService;
        }

        public void ValidateDTO(AddExamUserDTO dTO)
        {
            var exam = _examInternalService.GetById(dTO.ExamId);

            if (!exam.Published)
                throw new ApplicationValidationException($"Exam (id: {dTO.ExamId}) is not published");

            if (exam.End < DateTime.Now)
                throw new ApplicationValidationException($"the Exam (id: {dTO.ExamId}) has finished at {exam.End}");

            if (_examUserInternalService
                .GetIQueryable()
                .Where(x => x.UserId == dTO.UserId)
                .Where(x => x.ExamId == dTO.ExamId)
                .Any())
                throw new ApplicationValidationException($"User (id : {dTO.UserId}) has been registered at this Exam (id: {dTO.ExamId}) before");
        }
    }
}
