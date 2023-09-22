using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.AnswerDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Services.AnswerServices;
using OnlineExam.Application.Services.ExamServices;
using OnlineExam.Application.Services.ExamUserServices;
using OnlineExam.Application.Services.QuestionServices;
using OnlineExam.Application.Services.SectionServices;
using OnlineExam.Infrastructure.Contract.IRepositories;

namespace OnlineExam.Application.Validators
{
    internal class DatabaseBasedAnswerValidator : IDatabaseBasedAnswerValidator
    {
        readonly AnswerInternalService _answerInternalService;
        readonly ExamUserInternalService _examUserInternalService;
        readonly QuestionInternalService _questionInternalService;
        readonly SectionInternalService _sectionInternalService;
        readonly ExamInternalService _examInternalService;

        public void ValidateBeforeAdd(AddAnswerDTO dTO)
        {
            var examUser = _examUserInternalService.GetById(dTO.ExamUserId);
            var question = _questionInternalService.GetById(dTO.QuestionId);
            var section = _sectionInternalService.GetById(question!.SectionId);

            if (examUser.ExamId != section.ExamId)
                throw new ApplicationValidationException($"ExamUser by id: {dTO.ExamUserId} is not meant for Exam by id: {section.ExamId}");


            var exam = _examInternalService.GetById(examUser.ExamId);

            if (!exam.Published)
                throw new ApplicationValidationException($"Exam by id: {exam.Id} is not published yet");

            if (exam.Start > DateTime.Now)
                throw new ApplicationValidationException($"Exam by id: {exam.Id} is not Started yet");

            if (exam.End < DateTime.Now)
                throw new ApplicationValidationException($"Exam by id: {exam.Id} has ended");
        }

        public void ValidateBeforeUpdate(UpdateAnswerDTO dTO)
        {
            var answer = _answerInternalService.GetById(dTO.Id);
            var question = _questionInternalService.GetById(answer.QuestionId);

            if (question.Score < dTO.EarnedScore)
                throw new ApplicationValidationException($"EarnedScore for question (questionId: {question.Id}) can not be more than {question.Score}");
        }
    }
}
