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
        readonly IExamUserInternalService _examUserInternalService;
        readonly IExamUserMapper _mapper;
        readonly IDatabaseBasedExamUserValidator _validator;
        readonly IExamInternalService _examInternalService;

        public ExamUserService(IExamUserInternalService examUserService,
                               IExamUserMapper mapper,
                               IDatabaseBasedExamUserValidator validator,
                               IExamInternalService examInternalService)
        {
            _examUserInternalService = examUserService;
            _mapper = mapper;
            _validator = validator;
            _examInternalService = examInternalService;
        }

        public ShowExamUserDTO Add(AddExamUserDTO dTO)
        {
            _validator.ValidateBeforeAdd(dTO);

            var newExamUser = _mapper.AddDTOToEntity(dTO, DateTime.Now)!;

            _examUserInternalService.Add(newExamUser);

            return _mapper.EntityToShowDTO(newExamUser)!;
        }

        public void Delete(int examUserId, string issuerUserId)
        {
            var examUser = _examUserInternalService.GetById(examUserId);

            _validator.ThrowIfUserIsNotCreatorOfExamUser(issuerUserId, examUser);

            _examUserInternalService.Delete(examUser);
        }

        public void Finish(int id, string issuerUserId)
        {
            var examUser = _examUserInternalService.GetById(id);

            _validator.ThrowIfUserIsNotCreatorOfExamUser(issuerUserId, examUser);

            _validator.ValidateIfExamUserCanFinish(issuerUserId, examUser);

            examUser.End = DateTime.Now;

            _examUserInternalService.Update(examUser);
        }

        public IEnumerable<ShowExamUserDTO> GetAllByExamId(int examId, string issuerUserId, int skip = 0, int take = 20)
        {
            _validator.ThrowIfUserIsNotCreatorOfExam(examId, issuerUserId);

            return _examUserInternalService.GetAllByParentId(examId, skip, take).Select(_mapper.EntityToShowDTO)!;
        }

        public ShowExamUserDTO? GetById(int examUserId, string issuerUserId)
        {
            var examUser = _examUserInternalService.GetById(examUserId);

            _validator.ThrowIfUserIsNotCreatorOfExamUserOrExam(issuerUserId, examUser);

            return _mapper.EntityToShowDTO(examUser);
        }
    }
}
