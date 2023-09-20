using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.ExamUserDTOs;
using OnlineExam.Application.Contract.IServices;

namespace OnlineExam.Application.Services
{
    public sealed class ExamUserService : IExamUserService
    {
        readonly ExamUserInternalService _examUserService;
        readonly IExamUserMapper _mapper;
        readonly IDatabaseBasedExamUserValidator _validator;

        public ExamUserService(ExamUserInternalService examUserService, IExamUserMapper mapper, IDatabaseBasedExamUserValidator validator)
        {
            _examUserService = examUserService;
            _mapper = mapper;
            _validator = validator;
        }

        public ShowExamUserDTO Add(AddExamUserDTO dTO)
        {
            _validator.DatabaseBasedValidate(dTO);
            var newExamUser = _mapper.AddDTOToEntity(dTO, DateTime.Now)!;
            _examUserService.Add(newExamUser);
            return _mapper.EntityToShowDTO(newExamUser)!;
        }

        public void Delete(int examUserId)
            => _examUserService.Delete(examUserId);

        public IEnumerable<ShowExamUserDTO> GetAllByExamId(int examId, int skip = 0, int take = 20)
            => _examUserService.GetAllByParentId(examId, skip, take).Select(_mapper.EntityToShowDTO)!;

        public ShowExamUserDTO? GetById(int examUserId)
            => _mapper.EntityToShowDTO(_examUserService.GetById(examUserId));
    }
}
