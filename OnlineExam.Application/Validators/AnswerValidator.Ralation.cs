using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.AnswerDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    internal class AnswerRalationValidator : IAnswerRalationValidator
    {
        readonly IAnswerInternalService _answerInternalService;
        readonly IExamUserInternalService _examUserInternalService;
        readonly IQuestionInternalService _questionInternalService;

        public AnswerRalationValidator(
            IAnswerInternalService answerInternalService
            , IExamUserInternalService examUserInternalService
            , IQuestionInternalService questionInternalService)
        {
            _answerInternalService = answerInternalService;
            _examUserInternalService = examUserInternalService;
            _questionInternalService = questionInternalService;
        }

        public void ValidateBeforeAdd(Exam exam, IEnumerable<ExamUser> examUsers)
        {

        }

        public void ValidateBeforeUpdate(UpdateAnswerDTO dTO)
        {
            var answer = _answerInternalService.GetById(dTO.Id,
                _answerInternalService.GetIQueryable().Include(x => x.Question));

            if (answer.Question.Score < dTO.EarnedScore)
                throw new ApplicationValidationException($"EarnedScore for question (questionId: {answer.Question.Id}) can not be more than {answer.Question.Score}");
        }
    }
}
