using Microsoft.Extensions.DependencyInjection;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.ExamUserDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Services.UserServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.ExamUserServices
{
    public sealed class ExamUserService : IExamUserService
    {
        readonly IExamUserInternalService _examUserService;
        readonly IExamUserMapper _mapper;
        readonly IDatabaseBasedExamUserValidator _validator;
        readonly IExamInternalService _examInternalService;

        public ExamUserService(
                               IExamUserInternalService examUserService,
                               IExamUserMapper mapper,
                               IDatabaseBasedExamUserValidator validator,
                               IExamInternalService examInternalService)
        {
            _examUserService = examUserService;
            _mapper = mapper;
            _validator = validator;
            _examInternalService = examInternalService;
        }

        public ShowExamUserDTO Add(AddExamUserDTO dTO)
        {
            _validator.DatabaseBasedValidate(dTO);
            var newExamUser = _mapper.AddDTOToEntity(dTO, DateTime.Now)!;
            _examUserService.Add(newExamUser);
            return _mapper.EntityToShowDTO(newExamUser)!;
        }

        public void Delete(int examUserId, string issuerUserId)
        {
            var examUser = _examUserService.GetById(examUserId);

            if (examUser.UserId != issuerUserId && _examInternalService.GetById(examUser.ExamId).CreatorUserId != issuerUserId)
                throw new ApplicationUnAuthorizedException(
                string.Empty,
                new ApplicationValidationException(GenerateUnAuthorizedExceptionMessage(issuerUserId)));

            _examUserService.Delete(examUser);
        }

        public IEnumerable<ShowExamUserDTO> GetAllByExamId(int examId, string issuerUserId, int skip = 0, int take = 20)
        {
            if (_examInternalService.GetById(examId).CreatorUserId != issuerUserId)
                throw new ApplicationUnAuthorizedException(
                    string.Empty,
                    new ApplicationValidationException(GenerateUnAuthorizedExceptionMessage(examId, issuerUserId)));
            
            return _examUserService.GetAllByParentId(examId, skip, take).Select(_mapper.EntityToShowDTO)!;
        }

        public ShowExamUserDTO? GetById(int examUserId, string issuerUserId)
        {
            var examUser = _examUserService.GetById(examUserId);

            if(examUser.UserId != issuerUserId && _examInternalService.GetById(examUser.ExamId).CreatorUserId != issuerUserId)
                throw new ApplicationUnAuthorizedException(
                string.Empty,
                new ApplicationValidationException(GenerateUnAuthorizedExceptionMessage(issuerUserId)));

            return _mapper.EntityToShowDTO(examUser);
        }

        private string GenerateUnAuthorizedExceptionMessage(string issuerUserId)
            => $"User (id : {issuerUserId}) has no access to this ExamUser";
        
        private string GenerateUnAuthorizedExceptionMessage(int examId, string issuerUserId)
            => $"User (id : {issuerUserId}) is not the owner of exam (id : {examId})";
    }
}
