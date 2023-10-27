using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.AnswerDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Services.AnswerServices;
using OnlineExam.Application.Services.ExamUserServices;
using OnlineExam.Application.Services.QuestionServices;

namespace OnlineExam.Application.Validators
{
    internal class DatabaseBasedAnswerValidator : IDatabaseBasedAnswerValidator
    {
        readonly AnswerInternalService _answerInternalService;
        readonly ExamUserInternalService _examUserInternalService;
        readonly QuestionInternalService _questionInternalService;

        public DatabaseBasedAnswerValidator(AnswerInternalService answerInternalService, ExamUserInternalService examUserInternalService, QuestionInternalService questionInternalService)
        {
            _answerInternalService = answerInternalService;
            _examUserInternalService = examUserInternalService;
            _questionInternalService = questionInternalService;
        }

        public void ValidateBeforeAdd(AddAnswerDTO dTO)
        {
            var examUser = _examUserInternalService.GetById(dTO.ExamUserId,
                _examUserInternalService.GetIQueryable().Include(x => x.Exam));

            var question = _questionInternalService.GetById(dTO.QuestionId,
                _questionInternalService.GetIQueryable().Include(x => x.Section));


            if (examUser.ExamId != question.Section.ExamId)
                throw new ApplicationValidationException($"ExamUser by id: {dTO.ExamUserId} is not meant for Exam by id: {question.Section.ExamId}");


            if (!examUser.Exam.Published)
                throw new ApplicationValidationException($"Exam by id: {examUser.Exam.Id} is not published yet");

            if (examUser.Exam.Start > DateTime.Now)
                throw new ApplicationValidationException($"Exam by id: {examUser.Exam.Id} is not Started yet");

            if (examUser.Exam.End < DateTime.Now)
                throw new ApplicationValidationException($"Exam by id: {examUser.Exam.Id} has ended");
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
