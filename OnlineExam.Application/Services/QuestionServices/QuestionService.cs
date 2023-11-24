using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.QuestionDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.QuestionServices
{
    public sealed class QuestionService : IQuestionService
    {
        readonly IQuestionInternalService _questionInternalService;
        readonly IQuestionMapper _questionMapper;
        readonly IQuestionValidator _questionValidator;


        public QuestionService(IQuestionInternalService questionInternalService,
                               IQuestionMapper questionMapper,
                               IQuestionValidator questionValidator)
        {
            _questionInternalService = questionInternalService;
            _questionMapper = questionMapper;
            _questionValidator = questionValidator;
        }

        public ShowQuestionDTO Add(int sectionId, string issuerUserId, AddQuestionDTO dTO)
        {
            _questionValidator.ThrowIfUserIsNotExamCreator(sectionId, issuerUserId);

            var newQuestion = _questionMapper.AddDTOToEntity(sectionId, dTO);
            _questionInternalService.Add(newQuestion!);
            return _questionMapper.EntityToShowDTO(newQuestion)!;
        }

        public IEnumerable<ShowQuestionDTO> GetAllBySectionId(int sectionId, string issuerUserId, int skip, int take)
        {
            _questionValidator.ThrowIfUserIsNotExamCreatorOrExamUser(sectionId, issuerUserId);

            return _questionInternalService.GetAllByParentId(sectionId, skip, take).Select(_questionMapper.EntityToShowDTO)!;
        }

        public ShowQuestionDTO? GetById(int questionId, string issuerUserId)
        {
            var question = GetQuestionWith_Section_Exam_ExamUser_Included(questionId);

            _questionValidator.ThrowIfUserIsNotExamCreatorOrExamUser(issuerUserId, question.Section.Exam);

            return _questionMapper.EntityToShowDTO(question);
        }

        public void Update(int questionId, string issuerUserId, UpdateQuestionDTO dTO)
        {
            var question = GetQuestionWith_Section_Exam_ExamUser_Included(questionId);

            _questionValidator.ThrowIfUserIsNotExamCreator(issuerUserId, question.Section.Exam);

            _questionMapper.UpdateEntityByDTO(question, dTO);

            _questionInternalService.Update(question);
        }

        public void Delete(int questionId, string issuerUserId)
        {
            var question = GetQuestionWith_Section_Exam_ExamUser_Included(questionId);

            _questionValidator.ThrowIfUserIsNotExamCreator(issuerUserId, question.Section.Exam);

            _questionInternalService.Delete(question);
        }

        private Question GetQuestionWith_Section_Exam_ExamUser_Included(int questionId)
        {
            return _questionInternalService.GetById(questionId, _questionInternalService.GetIQueryable()
                                        .Include(x => x.Section)
                                        .ThenInclude(x => x.Exam)
                                        .ThenInclude(x => x.ExamUsers));
        }
    }
}
