using Microsoft.AspNetCore.Identity;
using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Contract.DTOs.ExamDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.ExamServices
{
    public sealed class ExamService : IExamService
    {
        readonly IExamInternalService _internalService;
        readonly IExamMapper _examMapper;

        public ExamService(IExamMapper examMapper, IExamInternalService internalService)
        {
            _examMapper = examMapper;
            _internalService = internalService;
        }

        public ShowExamDTO Add(AddExamDTO dTO)
        {
            var newExam = _examMapper.AddDTOToEntity(dTO);
            _internalService.Add(newExam);
            return _examMapper.EntityToShowDTO(newExam)!;
        }

        public void Delete(int id, string issuerUserId)
        {
            var exam = _internalService.GetById(id);

            if (exam.CreatorUserId != issuerUserId)
                throw new ApplicationUnAuthorizedException(
                    string.Empty,
                    new ApplicationValidationException(GenerateUnAuthorizedExceptionMessage(id, issuerUserId)));

            _internalService.Delete(exam);
        }

        public ShowExamDTO? GetById(int id) => _examMapper.EntityToShowDTO(_internalService.GetById(id));

        public IEnumerable<ShowExamDTO> GetAll(int skip, int take) => _internalService.GetAll(skip, take).Select(_examMapper.EntityToShowDTO)!;

        public void Update(int id, string issuerUserId, UpdateExamDTO dTO)
        {
            var exam = _internalService.GetById(id);

            if (exam.CreatorUserId != issuerUserId)
                throw new ApplicationUnAuthorizedException(
                    string.Empty,
                    new ApplicationValidationException(GenerateUnAuthorizedExceptionMessage(id, issuerUserId)));

            _examMapper.UpdateEntityByDTO(exam, dTO);

            _internalService.Update(exam);
        }

        private string GenerateUnAuthorizedExceptionMessage(int examId, string issuerUserId)
             => $"User (id : {issuerUserId}) is not the owner of exam (id : {examId})";
    }
}
