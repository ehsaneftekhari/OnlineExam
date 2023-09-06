using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services
{
    public class TextFieldService : ITextFieldService
    {
        readonly ITextFieldRepository _textFieldRepository;
        readonly ITextFieldMapper _textFieldMapper;
        readonly IQuestionRepository _questionRepository;
        readonly ITextFieldValidator _textFieldValidator;

        public TextFieldService(ITextFieldRepository textFieldRepository, ITextFieldMapper textFieldMapper, IQuestionRepository questionRepository, ITextFieldValidator textFieldValidator)
        {
            _textFieldRepository = textFieldRepository;
            _textFieldMapper = textFieldMapper;
            _questionRepository = questionRepository;
            _textFieldValidator = textFieldValidator;
        }

        public ShowTextFieldDTO Add(int questionId, AddTextFieldDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            if (questionId < 1)
                throw new ApplicationValidationException("QuestionId can not be less than 1");

            _textFieldValidator.ValidateDTO(dTO);

            try
            {
                var newTextField = _textFieldMapper.AddDTOToEntity(questionId, dTO)!;
                if (_textFieldRepository.Add(newTextField) > 0 && newTextField.Id > 0)
                    return _textFieldMapper.EntityToShowDTO(newTextField)!;

                throw new Exception();
            }
            catch
            {
                if (_questionRepository.GetById(questionId) == null)
                    throw new OEApplicationException($"Question with id:{questionId} is not exists");

                throw;
            }
        }

        public ShowTextFieldDTO? GetById(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var textField = _textFieldRepository.GetById(id);

            if (textField == null)
                throw new ApplicationSourceNotFoundException($"TextField with id:{id} is not exists");

            return _textFieldMapper.EntityToShowDTO(textField);
        }

        public IEnumerable<ShowTextFieldDTO> GetAllByQuestionId(int questionId, int skip = 0, int take = 20)
        {
            if (questionId < 1)
                throw new ApplicationValidationException("examId can not be less than 1");

            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var textFields =
                _textFieldRepository.GetIQueryable()
                .Where(q => q.QuestionId == questionId)
                .Skip(skip)
                .Take(take)
                .ToList()
                .Select(_textFieldMapper.EntityToShowDTO);

            if (!textFields.Any())
            {
                if (_questionRepository.GetById(questionId) == null)
                    throw new ApplicationSourceNotFoundException($"Question with id:{questionId} is not exists");

                throw new ApplicationSourceNotFoundException($"there is no TextField within Question (questionId:{questionId})");
            }

            return textFields!;
        }

        public void Update(int id, UpdateTextFieldDTO dTO)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            if (dTO == null)
                throw new ArgumentNullException();

            _textFieldValidator.ValidateDTO(dTO);

            var textField = _textFieldRepository.GetById(id);

            if (textField == null)
                throw new ApplicationSourceNotFoundException($"TextField with id:{id} is not exists");

            _textFieldMapper.UpdateEntityByDTO(textField, dTO);

            if (_textFieldRepository.Update(textField) <= 0)
                throw new Exception();
        }

        public void Delete(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var textField = _textFieldRepository.GetById(id);

            if (textField == null)
                throw new ApplicationSourceNotFoundException($"TextField with id:{id} is not exists");

            if (_textFieldRepository.Delete(textField) < 0)
                throw new Exception();
        }
    }
}
