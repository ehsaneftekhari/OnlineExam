using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Mappers;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services
{
    public class CheckFieldService : ICheckFieldService
    {
        readonly ICheckFieldRepository _checkFieldRepository;
        readonly IQuestionRepository _questionRepository;
        readonly ICheckFieldMapper _checkFieldMapper;

        public CheckFieldService(ICheckFieldRepository checkFieldRepository, IQuestionRepository questionRepository, ICheckFieldMapper checkFieldMapper)
        {
            _checkFieldRepository = checkFieldRepository;
            _questionRepository = questionRepository;
            _checkFieldMapper = checkFieldMapper;
        }

        public ShowCheckFieldDTO Add(int questionId, AddCheckFieldDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            if (questionId < 1)
                throw new ApplicationValidationException("QuestionId can not be less than 1");

            ValidateAddDTO(dTO);

            try
            {
                var newTextField = _checkFieldMapper.AddDTOToEntity(questionId, dTO)!;
                if (_checkFieldRepository.Add(newTextField) > 0 && newTextField.Id > 0)
                    return _checkFieldMapper.EntityToShowDTO(newTextField)!;

                throw new Exception();
            }
            catch
            {
                if (_questionRepository.GetById(questionId) == null)
                    throw new OEApplicationException($"Question with id:{questionId} is not exists");

                throw;
            }
        }

        public void Delete(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var textField = _checkFieldRepository.GetById(id);

            if (textField == null)
                throw new ApplicationSourceNotFoundException($"CheckField with id:{id} is not exists");

            if (_checkFieldRepository.Delete(textField) < 0)
                throw new Exception();
        }

        public IEnumerable<ShowCheckFieldDTO> GetAllByQuestionId(int questionId, int skip = 0, int take = 20)
        {
            if (questionId < 1)
                throw new ApplicationValidationException("examId can not be less than 1");

            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var checkFields =
                _checkFieldRepository.GetIQueryable()
                .Where(q => q.QuestionId == questionId)
                .Skip(skip)
                .Take(take)
                .ToList()
                .Select(_checkFieldMapper.EntityToShowDTO);

            if (!checkFields.Any())
            {
                if (_questionRepository.GetById(questionId) == null)
                    throw new ApplicationSourceNotFoundException($"Question with id:{questionId} is not exists");

                throw new ApplicationSourceNotFoundException($"there is no CheckField within Question (questionId:{questionId})");
            }

            return checkFields!;
        }

        public ShowCheckFieldDTO? GetById(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var textField = _checkFieldRepository.GetById(id);

            if (textField == null)
                throw new ApplicationSourceNotFoundException($"CheckField with id:{id} is not exists");

            return _checkFieldMapper.EntityToShowDTO(textField);
        }

        public void Update(int id, UpdateCheckFieldDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            ValidateAddDTO(dTO);

            var textField = _checkFieldRepository.GetById(id);

            if (textField == null)
                throw new ApplicationSourceNotFoundException($"TextField with id:{id} is not exists");

            _checkFieldMapper.UpdateEntityByDTO(textField, dTO);

            if (_checkFieldRepository.Update(textField) <= 0)
                throw new Exception();
        }

        private void ValidateAddDTO(AddCheckFieldDTO dTO)
            => ValidateDTO(dTO.MaximumSelection, dTO.CheckFieldUIType);

        private void ValidateAddDTO(UpdateCheckFieldDTO dTO)
            => ValidateDTO(dTO.MaximumSelection, dTO.CheckFieldUIType);

        private void ValidateDTO(int? maximumSelection, int? checkFieldUIType)
        {
            if (maximumSelection.HasValue && (maximumSelection < 1))
                throw new ApplicationValidationException("maximumSelection can not be less then 1");

            if (checkFieldUIType.HasValue && !Enum.IsDefined(typeof(CheckFieldUIType), checkFieldUIType))
                throw new ApplicationValidationException("checkFieldUIType is not valid");
        }
    }
}
