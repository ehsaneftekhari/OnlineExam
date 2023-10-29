using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.InternalService;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.AnswerDTOs;
using OnlineExam.Application.Contract.IServices;

namespace OnlineExam.Application.Services.AnswerServices
{
    public sealed class AnswerService : IAnswerService
    {
        readonly IAnswerInternalService _answerInternalService;
        readonly IAnswerMapper _answerMapper;
        readonly IAnswerValidator _answerValidator;
        readonly IDatabaseBasedAnswerValidator _databaseBasedAnswerValidator;

        public AnswerService(IAnswerInternalService answerInternalService,
                             IAnswerMapper answerMapper,
                             IAnswerValidator answerValidator,
                             IDatabaseBasedAnswerValidator databaseBasedAnswerValidator)
        {
            _answerInternalService = answerInternalService;
            _answerMapper = answerMapper;
            _answerValidator = answerValidator;
            _databaseBasedAnswerValidator = databaseBasedAnswerValidator;
        }

        public ShowAnswerDTO Add(AddAnswerDTO dTO)
        {
            _answerValidator.ValidateDTO(dTO);
            _databaseBasedAnswerValidator.ValidateBeforeAdd(dTO);
            var newAnswer = _answerMapper.AddDTOToEntity(dTO, DateTime.Now)!;
            _answerInternalService.Add(newAnswer);
            return _answerMapper.EntityToShowDTO(newAnswer)!;
        }

        public ShowAnswerDTO? GetById(int answerId)
            => _answerMapper.EntityToShowDTO(_answerInternalService.GetById(answerId));

        public IEnumerable<ShowAnswerDTO> GetAll(int examUserId, int questionId, int skip, int take)
            => _answerInternalService.GetAllByParentsIds(examUserId, questionId, skip, take).Select(_answerMapper.EntityToShowDTO)!;

        public IEnumerable<ShowAnswerDTO> GetAllByExamUserId(int examUserId, int skip, int take)
            => _answerInternalService.GetAllByExamUserId(examUserId, skip, take).Select(_answerMapper.EntityToShowDTO)!;

        public void UpdateEarnedScore(UpdateAnswerDTO dTO)
        {
            _answerValidator.ValidateDTO(dTO);
            _databaseBasedAnswerValidator.ValidateBeforeUpdate(dTO);
            var answer = _answerInternalService.GetById(dTO.Id);
            _answerMapper.UpdateEntityByDTO(answer, dTO);
            _answerInternalService.Update(answer);
        }
    }
}
