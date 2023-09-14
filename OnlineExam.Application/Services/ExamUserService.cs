using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.ExamUserDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Infrastructure.Contract.IRepositories;

namespace OnlineExam.Application.Services
{
    public class ExamUserService : IExamUserService
    {
        readonly IExamRepository _examRepository;
        readonly IExamUserMapper _mapper;
        readonly IExamUserRepository _repository;
        readonly IDatabaseBasedExamUserValidator _validator;

        public ExamUserService(IExamUserMapper mapper,
                               IExamUserRepository repository,
                               IDatabaseBasedExamUserValidator validator,
                               IExamRepository examRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _validator = validator;
            _examRepository = examRepository;
        }

        public ShowExamUserDTO Add(AddExamUserDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            _validator.DatabaseBasedValidate(dTO);

            var newExamUser = _mapper.AddDTOToEntity(dTO, DateTime.Now)!;
            if (_repository.Add(newExamUser) > 0 && newExamUser.Id > 0)
                return _mapper.EntityToShowDTO(newExamUser)!;

            throw new Exception();
        }

        public void Delete(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var examUser = _repository.GetById(id);

            if (examUser == null)
                throw new ApplicationSourceNotFoundException($"ExamUser with id:{id} is not exists");

            if (_repository.Delete(examUser) < 0)
                throw new Exception();
        }

        public IEnumerable<ShowExamUserDTO> GetAllByExamId(int examId, int skip = 0, int take = 20)
        {
            if (examId < 1)
                throw new ApplicationValidationException("examId can not be less than 1");

            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var sections =
                _repository.GetIQueryable()
                .Where(q => q.ExamId == examId)
                .Skip(skip)
                .Take(take)
                .ToList()
                .Select(_mapper.EntityToShowDTO);

            if (!sections.Any())
            {
                if (_examRepository.GetById(examId) == null)
                    throw new ApplicationSourceNotFoundException($"Exam with id:{examId} is not exists");

                throw new ApplicationSourceNotFoundException($"there is no ExamUser within Exam (examId:{examId})");
            }

            return sections!;
        }

        public ShowExamUserDTO? GetById(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var examUser = _repository.GetById(id);

            if (examUser == null)
                throw new ApplicationSourceNotFoundException($"ExamUser with id:{id} is not exists");

            return _mapper.EntityToShowDTO(examUser);
        }
    }
}
