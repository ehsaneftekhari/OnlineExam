using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.ExamUserDTOs;
using OnlineExam.Application.Contract.IServices;

namespace OnlineExam.Application.Services.ExamUserServices
{
    public sealed class ExamUserService : IExamUserService
    {
        readonly IExamUserInternalService _examUserInternalService;
        readonly IExamUserMapper _mapper;
        readonly IExamUserDTOValidator _examUserDTOValidator;
        readonly IExamUserAccessValidator _examUserAccessValidator;
        readonly IExamUserActionValidator _examUserActionValidator;

        public ExamUserService(IExamUserInternalService examUserService,
                               IExamUserMapper mapper,
                               IExamUserDTOValidator examUserDTOValidator,
                               IExamUserAccessValidator examUserAccessValidator,
                               IExamUserActionValidator examUserActionValidator)
        {
            _examUserInternalService = examUserService;
            _mapper = mapper;
            _examUserDTOValidator = examUserDTOValidator;
            _examUserAccessValidator = examUserAccessValidator;
            _examUserActionValidator = examUserActionValidator;
        }

        public ShowExamUserDTO Add(AddExamUserDTO dTO)
        {
            _examUserDTOValidator.ValidateDTO(dTO);

            var newExamUser = _mapper.AddDTOToEntity(dTO, DateTime.Now)!;

            _examUserInternalService.Add(newExamUser);

            return _mapper.EntityToShowDTO(newExamUser)!;
        }

        public void Delete(int examUserId, string issuerUserId)
        {
            var examUser = _examUserInternalService.GetById(examUserId);

            _examUserAccessValidator.ThrowIfUserIsNotCreatorOfExamUser(issuerUserId, examUser);

            _examUserInternalService.Delete(examUser);
        }

        public void Finish(int id, string issuerUserId)
        {
            var examUser = _examUserInternalService.GetById(id);

            _examUserAccessValidator.ThrowIfUserIsNotCreatorOfExamUser(issuerUserId, examUser);

            _examUserActionValidator.ValidateIfExamUserCanFinish(examUser);

            examUser.End = DateTime.Now;

            _examUserInternalService.Update(examUser);
        }

        public IEnumerable<ShowExamUserDTO> GetAllByExamId(int examId, string issuerUserId, int skip = 0, int take = 20)
        {
            _examUserAccessValidator.ThrowIfUserIsNotCreatorOfExam(examId, issuerUserId);

            return _examUserInternalService.GetAllByParentId(examId, skip, take).Select(_mapper.EntityToShowDTO)!;
        }

        public ShowExamUserDTO? GetById(int examUserId, string issuerUserId)
        {
            var examUser = _examUserInternalService.GetById(examUserId);

            _examUserAccessValidator.ThrowIfUserIsNotCreatorOfExamUserOrExam(issuerUserId, examUser);

            return _mapper.EntityToShowDTO(examUser);
        }
    }
}
