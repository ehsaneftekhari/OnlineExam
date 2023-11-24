using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.QuestionDTOs;
using OnlineExam.Application.Contract.Exceptions;
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

            ThrowIfUserIsNotExamCreator(issuerUserId, exam);

            var newQuestion = _questionMapper.AddDTOToEntity(sectionId, dTO);
            _questionInternalService.Add(newQuestion!);
            return _questionMapper.EntityToShowDTO(newQuestion)!;
        }


        public IEnumerable<ShowQuestionDTO> GetAllBySectionId(int sectionId, string issuerUserId, int skip, int take)
        {
            var exam = _sectionInternalService
                .GetById(sectionId, _sectionInternalService
                                        .GetIQueryable()
                                        .Include(x => x.Exam)
                                        .ThenInclude(x => x.ExamUsers)).Exam;

            ThrowIfUserIsNotExamCreatorOrExamUser(issuerUserId, exam);

            return _questionInternalService.GetAllByParentId(sectionId, skip, take).Select(_questionMapper.EntityToShowDTO)!;
        }


        public ShowQuestionDTO? GetById(int questionId, string issuerUserId)
        {
            var question = GetQuestionWith_Section_Exam_ExamUser_Included(questionId);

            ThrowIfUserIsNotExamCreatorOrExamUser(issuerUserId, question.Section.Exam);

            return _questionMapper.EntityToShowDTO(question);
        }


        public void Update(int questionId, string issuerUserId, UpdateQuestionDTO dTO)
        {
            var question = GetQuestionWith_Section_Exam_ExamUser_Included(questionId);

            ThrowIfUserIsNotExamCreator(issuerUserId, question.Section.Exam);

            _questionMapper.UpdateEntityByDTO(question, dTO);

            _questionInternalService.Update(question);
        }

        public void Delete(int questionId, string issuerUserId)
        {
            var question = GetQuestionWith_Section_Exam_ExamUser_Included(questionId);

            ThrowIfUserIsNotExamCreator(issuerUserId, question.Section.Exam);

            _questionInternalService.Delete(question);
        }

        private Question GetQuestionWith_Section_Exam_ExamUser_Included(int questionId)
        {
            return _questionInternalService.GetById(questionId, _questionInternalService.GetIQueryable()
                                        .Include(x => x.Section)
                                        .ThenInclude(x => x.Exam)
                                        .ThenInclude(x => x.ExamUsers));
        }

        private static void ThrowIfUserIsNotExamCreatorOrExamUser(string issuerUserId, Exam exam)
        {
            if (exam.CreatorUserId != issuerUserId
                && !exam.ExamUsers.Any(x => x.UserId == issuerUserId))
                throw new ApplicationUnAuthorizedException($"User has no access to Question");
        }

        private void ThrowIfUserIsNotExamCreator(string issuerUserId, Exam exam)
        {
            _examValidator.ThrowIfUserIsNotExamCreator(issuerUserId, exam);
        }
    }
}
