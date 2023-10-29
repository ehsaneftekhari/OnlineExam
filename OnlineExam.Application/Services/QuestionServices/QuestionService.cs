using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.InternalService;
using OnlineExam.Application.Contract.DTOs.QuestionDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.QuestionServices
{
    public sealed class QuestionService : IQuestionService
    {
        readonly IBaseInternalService<Question, Section> _questionInternalService;
        readonly IQuestionMapper _questionMapper;

        public QuestionService(IBaseInternalService<Question, Section> questionInternalService, IQuestionMapper questionMapper)
        {
            _questionInternalService = questionInternalService;
            _questionMapper = questionMapper;
        }

        public ShowQuestionDTO Add(int sectionId, AddQuestionDTO dTO)
        {
            var newQuestion = _questionMapper.AddDTOToEntity(sectionId, dTO);
            _questionInternalService.Add(newQuestion!);
            return _questionMapper.EntityToShowDTO(newQuestion)!;
        }

        public IEnumerable<ShowQuestionDTO> GetAllBySectionId(int sectionId, int skip, int take)
            => _questionInternalService.GetAllByParentId(sectionId, skip, take).Select(_questionMapper.EntityToShowDTO)!;

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
