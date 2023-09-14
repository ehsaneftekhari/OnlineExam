using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Contract.DTOs.QuestionDTOs;
using OnlineExam.Application.Contract.IServices;

namespace OnlineExam.Application.Services.QuestionServices
{
    public class QuestionService : IQuestionService
    {
        readonly QuestionInternalService _questionInternalService;
        readonly IQuestionMapper _questionMapper;

        public QuestionService(QuestionInternalService questionInternalService, IQuestionMapper questionMapper)
        {
            _questionInternalService = questionInternalService;
            _questionMapper = questionMapper;
        }

        public ShowQuestionDTO Add(int sectionId, AddQuestionDTO question)
        {
            var newQuestion = _questionMapper.AddDTOToEntity(sectionId, question);
            _questionInternalService.Add(sectionId, newQuestion!);
            return _questionMapper.EntityToShowDTO(newQuestion)!;
        }

        public IEnumerable<ShowQuestionDTO> GetAllBySectionId(int sectionId, int skip, int take)
            => _questionInternalService.GetAllBySectionId(sectionId, skip, take).Select(_questionMapper.EntityToShowDTO)!;

        public ShowQuestionDTO? GetById(int questionId)
            => _questionMapper.EntityToShowDTO(_questionInternalService.GetById(questionId));

        public void Update(int questionId, UpdateQuestionDTO dTO)
        {
            var question = _questionInternalService.GetById(questionId);

            _questionMapper.UpdateEntityByDTO(question, dTO);

            _questionInternalService.Update(question);
        }

        public void Delete(int questionId)
            => _questionInternalService.Delete(questionId);
    }
}
