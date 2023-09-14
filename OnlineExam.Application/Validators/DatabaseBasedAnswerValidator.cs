using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.AnswerDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Infrastructure.Contract.IRepositories;

namespace OnlineExam.Application.Validators
{
    internal class DatabaseBasedAnswerValidator : IDatabaseBasedAnswerValidator
    {
        readonly IAnswerRepository _answerRepository;
        readonly IExamService _examService;
        readonly ISectionService _sectionService;
        readonly IExamUserService _examUserService;
        readonly IQuestionService _questionService;

        public DatabaseBasedAnswerValidator(IAnswerRepository answerRepository,
                                            IExamService examService,
                                            ISectionService sectionService,
                                            IExamUserService examUserService,
                                            IQuestionService questionService)
        {
            _answerRepository = answerRepository;
            _examService = examService;
            _sectionService = sectionService;
            _examUserService = examUserService;
            _questionService = questionService;
        }

        public void ValidateBeforeAdd(AddAnswerDTO dTO)
        {
            var examUser = _examUserService.GetById(dTO.ExamUserId);
            var question = _questionService.GetById(dTO.QuestionId);
            var section = _sectionService.GetById(question!.SectionId);

            if (examUser.ExamId != section.ExamId)
                throw new ApplicationValidationException($"ExamUser by id: {dTO.ExamUserId} is not meant for Exam by id: {section.ExamId}");


            var exam = _examService.GetById(examUser.ExamId);

            if (!exam.Published)
                throw new ApplicationValidationException($"Exam by id: {exam.Id} is not published yet");

            if (exam.Start > DateTime.Now)
                throw new ApplicationValidationException($"Exam by id: {exam.Id} is not Started yet");

            if (exam.End < DateTime.Now)
                throw new ApplicationValidationException($"Exam by id: {exam.Id} has ended");
        }

        public void ValidateBeforeUpdate(UpdateAnswerDTO dTO)
        {
            var answer = _answerRepository.GetById(dTO.Id);
            var question = _questionService.GetById(answer.QuestionId);

            if (question.Score < dTO.EarnedScore)
                throw new ApplicationValidationException($"EarnedScore for question (questionId: {question.Id}) can not be more than {question.Score}");
        }
    }
}
