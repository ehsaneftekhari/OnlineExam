using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.AnswerDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Mappers;
using OnlineExam.Infrastructure.Contract.IRepositories;

namespace OnlineExam.Application.Services
{
    public class AnswerService : IAnswerService
    {
        readonly IAnswerRepository _answerRepository;
        readonly IAnswerMapper _answerMapper;
        readonly IAnswerValidator _answerValidator;
        readonly IDatabaseBasedAnswerValidator _databaseBasedAnswerValidator;

        public AnswerService(IAnswerRepository answerRepository, IAnswerMapper answerMapper)
        {
            _answerRepository = answerRepository;
            _answerMapper = answerMapper;
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

        public void UpdateEarnedScore(UpdateAnswerDTO dTO)
        {
            _answerValidator.ValidateDTO(dTO);

            var answer = _answerRepository.GetById(id);

            if (answer == null)
                throw new ApplicationSourceNotFoundException($"Exam with id:{id} is not exists");

            _examMapper.UpdateEntityByDTO(answer, dTO);

            if (_examRepository.Update(answer) <= 0)
                throw new Exception();
        }
    }
}
