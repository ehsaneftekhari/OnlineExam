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
        readonly IQuestionAccessValidator _questionAccessValidator;


        public QuestionService(IQuestionInternalService questionInternalService,
                               IQuestionMapper questionMapper,
                               IQuestionAccessValidator questionAccessValidator)
        {
            _questionInternalService = questionInternalService;
            _questionMapper = questionMapper;
            _questionAccessValidator = questionAccessValidator;
        }

        public ShowQuestionDTO Add(int sectionId, string issuerUserId, AddQuestionDTO dTO)
        {
            _questionAccessValidator.ThrowIfUserIsNotExamCreator(sectionId, issuerUserId);

            var newQuestion = _questionMapper.AddDTOToEntity(sectionId, dTO);
            _questionInternalService.Add(newQuestion!);
            return _questionMapper.EntityToShowDTO(newQuestion)!;
        }

        public IEnumerable<ShowQuestionDTO> GetAllBySectionId(int sectionId, string issuerUserId, int skip, int take)
        {
            _questionAccessValidator.ThrowIfUserIsNotExamCreatorOrExamUser(sectionId, issuerUserId);

            return _questionInternalService.GetAllByParentId(sectionId, skip, take).Select(_questionMapper.EntityToShowDTO)!;
        }

        public ShowQuestionDTO? GetById(int questionId, string issuerUserId)
        {
            var question = _questionInternalService.GetWith_Section_Exam_ExamUser_Included(questionId);

            _questionAccessValidator.ThrowIfUserIsNotExamCreatorOrExamUser(issuerUserId, question.Section.Exam);

            return _questionMapper.EntityToShowDTO(question);
        }

        public void Update(int questionId, string issuerUserId, UpdateQuestionDTO dTO)
        {
            var question = _questionInternalService.GetWith_Section_Exam_ExamUser_Included(questionId);

            _questionAccessValidator.ThrowIfUserIsNotExamCreator(issuerUserId, question.Section.Exam);

            _questionMapper.UpdateEntityByDTO(question, dTO);

            _questionInternalService.Update(question);
        }

        public void Delete(int questionId, string issuerUserId)
        {
            var question = _questionInternalService.GetWith_Section_Exam_ExamUser_Included(questionId);

            _questionAccessValidator.ThrowIfUserIsNotExamCreator(issuerUserId, question.Section.Exam);

            _questionInternalService.Delete(question);
        }
    }
}
