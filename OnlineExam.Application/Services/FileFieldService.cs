using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.FileFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Mappers;
using OnlineExam.Application.Validators;
using OnlineExam.Infrastructure.Contract.IRepositories;

namespace OnlineExam.Application.Services
{
    public class FileFieldService : IFileFieldService
    {
        readonly IFileFieldRepository _fileFieldRepository;
        readonly AllowedFileTypesFieldService _allowedFileTypesFieldInternalService;
        readonly IQuestionRepository _questionRepository;
        readonly IFileFieldMapper _fileFieldMapper;
        readonly IFileFieldValidator _fileFieldValidator;

        public FileFieldService(IFileFieldRepository optionRepository,
                                IQuestionRepository questionRepository,
                                IFileFieldMapper fileFieldMapper,
                                IFileFieldValidator fileFieldValidator,
                                AllowedFileTypesFieldService allowedFileTypesFieldInternalService)
        {
            _fileFieldRepository = optionRepository;
            _questionRepository = questionRepository;
            _fileFieldMapper = fileFieldMapper;
            _fileFieldValidator = fileFieldValidator;
            _allowedFileTypesFieldInternalService = allowedFileTypesFieldInternalService;
        }

        public ShowFileFieldDTO Add(int questionId, AddFileFieldDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            if (questionId < 1)
                throw new ApplicationValidationException("QuestionId can not be less than 1");

            _fileFieldValidator.ValidateDTO(dTO);


            var newFileField = _fileFieldMapper.AddDTOToEntity(questionId, dTO, _allowedFileTypesFieldInternalService.GetByIds)!;
            if (_fileFieldRepository.Add(newFileField) > 0 && newFileField.Id > 0)
                return _fileFieldMapper.EntityToShowDTO(newFileField)!;

            throw new Exception();
        }

        public void Delete(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var fileField = _fileFieldRepository.GetById(id);

            if (fileField == null)
                throw new ApplicationSourceNotFoundException($"FileField with id:{id} is not exists");

            if (_fileFieldRepository.Delete(fileField) < 0)
                throw new Exception();
        }

        public IEnumerable<ShowFileFieldDTO> GetAllByQuestionId(int questionId, int skip = 0, int take = 20)
        {
            if (questionId < 1)
                throw new ApplicationValidationException("examId can not be less than 1");

            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var fileFields =
                _fileFieldRepository.GetIQueryable()
                .Where(q => q.QuestionId == questionId)
                .Skip(skip)
                .Take(take)
                .ToList()
                .Select(_fileFieldMapper.EntityToShowDTO);

            if (!fileFields.Any())
            {
                if (_questionRepository.GetById(questionId) == null)
                    throw new ApplicationSourceNotFoundException($"Question with id:{questionId} is not exists");

                throw new ApplicationSourceNotFoundException($"there is no FileField within Question (questionId:{questionId})");
            }

            return fileFields!;
        }

        public ShowFileFieldDTO? GetById(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var fileField = _fileFieldRepository.GetById(id);

            if (fileField == null)
                throw new ApplicationSourceNotFoundException($"FileField with id:{id} is not exists");

            return _fileFieldMapper.EntityToShowDTO(fileField);
        }

        public void Update(int id, UpdateFileFieldDTO dTO)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            if (dTO == null)
                throw new ArgumentNullException();

            _fileFieldValidator.ValidateDTO(dTO);

            var fileField = _fileFieldRepository.GetById(id);

            if (fileField == null)
                throw new ApplicationSourceNotFoundException($"FileField with id:{id} is not exists");

            _fileFieldMapper.UpdateEntityByDTO(fileField, dTO, _allowedFileTypesFieldInternalService.GetByIds);

            if (_fileFieldRepository.Update(fileField) <= 0)
                throw new Exception();
        }
    }
}
