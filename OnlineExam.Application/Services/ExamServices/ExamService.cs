using Microsoft.AspNetCore.Identity;
using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.ExamDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.ExamServices
{
    public sealed class ExamService : IExamService
    {
        readonly IExamInternalService _examInternalService;
        readonly IExamMapper _examMapper;
        readonly IExamValidator _examValidator;

        public ExamService(IExamMapper examMapper,
                           IExamInternalService internalService,
                           IExamValidator examValidator)
        {
            _examMapper = examMapper;
            _examInternalService = internalService;
            _examValidator = examValidator;
        }

        public ShowExamDTO Add(AddExamDTO dTO)
        {
            var newExam = _examMapper.AddDTOToEntity(dTO);

            _examInternalService.Add(newExam);

            return _examMapper.EntityToShowDTO(newExam)!;
        }

        public void Delete(int id, string issuerUserId)
        {
            var exam = _examInternalService.GetById(id);

            _examValidator.ThrowIfUserIsNotExamCreator(issuerUserId, exam);

            _examInternalService.Delete(exam);
        }

        public ShowExamDTO? GetById(int id) 
            => _examMapper.EntityToShowDTO(_examInternalService.GetById(id));

        public IEnumerable<ShowExamDTO> GetAll(int skip, int take) 
            => _examInternalService.GetAll(skip, take).Select(_examMapper.EntityToShowDTO)!;

        public void Update(int id, string issuerUserId, UpdateExamDTO dTO)
        {
            var exam = _examInternalService.GetById(id);

            _examValidator.ThrowIfUserIsNotExamCreator(issuerUserId, exam);

            _examMapper.UpdateEntityByDTO(exam, dTO);

            _examInternalService.Update(exam);
        }
    }
}
