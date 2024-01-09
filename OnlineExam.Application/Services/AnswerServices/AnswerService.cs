using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.AnswerDTOs;
using OnlineExam.Application.Contract.IServices;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.AnswerServices
{
    public sealed class AnswerService : IAnswerService
    {
        readonly IAnswerInternalService _answerInternalService;
        readonly IQuestionInternalService _questionInternalService;
        readonly IExamAccessValidator _examAccessValidator;
        readonly IAnswerMapper _answerMapper;
        readonly IAnswerDTOValidator _answerValidator;
        readonly IDatabaseBasedAnswerValidator _databaseBasedAnswerValidator;
        readonly IAnswerAccessValidator _answerAccessValidator;

        public AnswerService(IAnswerInternalService answerInternalService,
                             IQuestionInternalService questionInternalService,
                             IAnswerMapper answerMapper,
                             IAnswerDTOValidator answerValidator,
                             IDatabaseBasedAnswerValidator databaseBasedAnswerValidator,
                             IExamAccessValidator examAccessValidator,
                             IAnswerAccessValidator answerAccessValidator)
        {
            _answerInternalService = answerInternalService;
            _questionInternalService = questionInternalService;
            _answerMapper = answerMapper;
            _answerValidator = answerValidator;
            _databaseBasedAnswerValidator = databaseBasedAnswerValidator;
            _examAccessValidator = examAccessValidator;
            _answerAccessValidator = answerAccessValidator;
        }

        public ShowAnswerDTO Add(AddAnswerDTO dTO, string issuerUserId)
        {
            _answerValidator.ValidateDTO(dTO);

            var now = DateTime.Now;

            var exam = _questionInternalService.GetIQueryable()
                .Where(q => q.Id == dTO.QuestionId)
                .Include(q => q.Section)
                .ThenInclude(q => q.Exam)
                .ThenInclude(x => x.ExamUsers.Where(eu => eu.UserId == issuerUserId))
                .Select(x => x.Section.Exam)
                .FirstOrDefault();

            if (exam == null)
                _questionInternalService.IsNotExistsException();

            _answerValidator.ThrowIfExamIsNotActive(now, exam!);

            _answerValidator.ThrowIfExamUsersAreNotValid(exam!);

            var newAnswer = _answerMapper.AddDTOToEntity(dTO, exam!.ExamUsers.Single().Id, now)!;

            _answerInternalService.Add(newAnswer);

            return _answerMapper.EntityToShowDTO(newAnswer)!;
        }

        public ShowAnswerDTO? GetById(int answerId, string issuerUserId)
        {
            var answer = GetAnswer_withExamUser_Exam_Included(answerId);

            if(!_answerAccessValidator.IsUserExamUserOrExamCreator(answer!.ExamUser, issuerUserId))
                throw new ApplicationUnAuthorizedException("User has no access to Answer");

            return _answerMapper.EntityToShowDTO(answer);
        }

        public IEnumerable<ShowAnswerDTO> GetAll(int examUserId, int questionId, string issuerUserId, int skip, int take)
        {
            var answers = _answerInternalService.GetAllByParentsIds(
                examUserId,
                questionId,
                q => q.Include(x => x.ExamUser)
                    .ThenInclude(x => x.Exam),
                skip,
                take);

            if (!_answerAccessValidator.IsUserExamUserOrExamCreator(answers.Any() ? answers.FirstOrDefault()!.ExamUser : null, issuerUserId))
                throw new ApplicationUnAuthorizedException("User has no access to ExamUser");

            return answers.Select(_answerMapper.EntityToShowDTO)!;
        }

        public IEnumerable<ShowAnswerDTO> GetAllByExamUserId(int examUserId, string issuerUserId, int skip, int take)
        {
            var answers = _answerInternalService.GetAllByExamUserId(
                examUserId,
                q => q.Include(x => x.ExamUser),
                skip,
                take);

            if(!_answerAccessValidator.IsUserExamUserOrExamCreator(answers.Any() ? answers.FirstOrDefault()!.ExamUser : null, issuerUserId))
                throw new ApplicationUnAuthorizedException("User has no access to ExamUser");

            return answers.Select(_answerMapper.EntityToShowDTO)!;
        }

        public void UpdateEarnedScore(UpdateAnswerDTO dTO, string issuerUserId)
        {
            _answerValidator.ValidateDTO(dTO);
            _databaseBasedAnswerValidator.ValidateBeforeUpdate(dTO);

            var answer = GetAnswer_withExamUser_Exam_Included(dTO.Id);

            if(!_answerAccessValidator.IsUserExamCreator(answer!.ExamUser, issuerUserId))
                throw new ApplicationUnAuthorizedException("User has no access to Answer");

            _answerMapper.UpdateEntityByDTO(answer, dTO);

            _answerInternalService.Update(answer);
        }

        private Answer GetAnswer_withExamUser_Exam_Included(int answerId)
        {
            return _answerInternalService.GetById(
                answerId,
                qu => qu.Include(a => a.ExamUser)
                    .ThenInclude(x => x.Exam));
        }
    }
}
