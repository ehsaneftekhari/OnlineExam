﻿using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.AnswerDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Infrastructure.Contract.IRepositories;

namespace OnlineExam.Application.Services
{
    public class AnswerService : IAnswerService
    {
        readonly IAnswerRepository _answerRepository;
        readonly IAnswerMapper _answerMapper;
        readonly IAnswerValidator _answerValidator;
        readonly IDatabaseBasedAnswerValidator _databaseBasedAnswerValidator;
        readonly IExamUserService _examUserService;
        readonly IQuestionService _questionService;

        public AnswerService(IAnswerRepository answerRepository,
                             IAnswerMapper answerMapper,
                             IAnswerValidator answerValidator,
                             IDatabaseBasedAnswerValidator databaseBasedAnswerValidator,
                             IExamUserService examUserService,
                             IQuestionService questionService)
        {
            _answerRepository = answerRepository;
            _answerMapper = answerMapper;
            _answerValidator = answerValidator;
            _databaseBasedAnswerValidator = databaseBasedAnswerValidator;
            _examUserService = examUserService;
            _questionService = questionService;
        }

        public ShowAnswerDTO Add(AddAnswerDTO dTO)
        {
            _answerValidator.ValidateDTO(dTO);
            _databaseBasedAnswerValidator.ValidateBeforeAdd(dTO);

            var newAnswer = _answerMapper.AddDTOToEntity(dTO, DateTime.Now)!;
            if (_answerRepository.Add(newAnswer) > 0 && newAnswer.Id > 0)
                return _answerMapper.EntityToShowDTO(newAnswer)!;

            throw new OEApplicationException("answer did not saved");
        }

        public ShowAnswerDTO? GetById(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var answer = _answerRepository.GetById(id);

            if (answer == null)
                throw new ApplicationSourceNotFoundException($"Answer with id:{id} is not exists");

            return _answerMapper.EntityToShowDTO(answer);
        }

        public IEnumerable<ShowAnswerDTO> GetAll(int examUserId, int questionId, int skip, int take)
        {
            if (examUserId < 1)
                throw new ApplicationValidationException("examUserId can not be less than 1");
            
            if (questionId < 1)
                throw new ApplicationValidationException("questionId can not be less than 1");

            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var answer =
                _answerRepository.GetIQueryable()
                .Where(a => a.ExamUserId == examUserId)
                .Where(a => a.QuestionId == questionId)
                .Skip(skip)
                .Take(take)
                .ToList()
                .Select(_answerMapper.EntityToShowDTO);

            if (!answer.Any())
            {
                var examUser = _examUserService.GetById(examUserId);
                var question = _questionService.GetById(questionId);

                throw new ApplicationSourceNotFoundException($"there is no answers for question (questionId:{question}) by mentioned exam user (examUserId: {examUserId})");
            }

            return answer!;
        }

        public void UpdateEarnedScore(UpdateAnswerDTO dTO)
        {
            _answerValidator.ValidateDTO(dTO);
            _databaseBasedAnswerValidator.ValidateBeforeUpdate(dTO);

            var answer = _answerRepository.GetById(dTO.Id);

            if (answer == null)
                throw new ApplicationSourceNotFoundException($"Answer with id:{dTO.Id} is not exists");

            _answerMapper.UpdateEntityByDTO(answer, dTO);

            if (_answerRepository.Update(answer) <= 0)
                throw new Exception();
        }
    }
}
