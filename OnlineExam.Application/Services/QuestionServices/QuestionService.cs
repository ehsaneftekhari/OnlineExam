using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.QuestionDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Services.SectionServices;
using OnlineExam.Application.Validators;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.QuestionServices
{
    public sealed class QuestionService : IQuestionService
    {
        readonly IQuestionInternalService _questionInternalService;
        readonly IQuestionMapper _questionMapper;
        readonly ISectionInternalService _sectionInternalService;
        readonly IExamValidator _examValidator;

        public QuestionService(IQuestionInternalService questionInternalService, IQuestionMapper questionMapper, ISectionInternalService sectionInternalService, IExamValidator examValidator)
        {
            _questionInternalService = questionInternalService;
            _questionMapper = questionMapper;
            _sectionInternalService = sectionInternalService;
            _examValidator = examValidator;
        }

        public ShowQuestionDTO Add(int sectionId, string issuerUserId, AddQuestionDTO dTO)
        {
            var exam = _sectionInternalService
                .GetById(sectionId, _sectionInternalService
                                        .GetIQueryable()
                                        .Include(x => x.Exam)).Exam;

            _examValidator.ThrowIfUserIsNotExamCreator(issuerUserId, exam);

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
