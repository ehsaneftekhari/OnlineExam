using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.IMappers;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services
{
    public class TextFieldService : ITextFieldService
    {
        readonly ITextFieldRepository _textFieldRepository;
        readonly ITextFieldMapper _textFieldMapper;
        readonly IQuestionRepository _questionRepository;

        public TextFieldService(ITextFieldRepository textFieldRepository, ITextFieldMapper textFieldMapper, IQuestionRepository questionRepository)
        {
            _textFieldRepository = textFieldRepository;
            _textFieldMapper = textFieldMapper;
            _questionRepository = questionRepository;
        }

        public ShowTextFieldDTO Add(int questionId, AddTextFieldDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            if (questionId < 1)
                throw new ApplicationValidationException("QuestionId can not be less than 1");

            ValidateAddDTO(dTO);

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
                    throw new OEApplicationException($"QuestionId with id:{questionId} is not exists");

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

        public IEnumerable<ShowTextFieldDTO> GetAllByExamId(int questionId, int skip = 0, int take = 20)
        {
            if (questionId < 1)
                throw new ApplicationValidationException("examId can not be less than 1");

            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var sections =
                _textFieldRepository.GetIQueryable()
                .Where(q => q.QuestionId == questionId)
                .Skip(skip)
                .Take(take)
                .ToList()
                .Select(_textFieldMapper.EntityToShowDTO);

            if (!sections.Any())
            {
                if (_questionRepository.GetById(questionId) == null)
                    throw new ApplicationSourceNotFoundException($"QuestionId with id:{questionId} is not exists");

                throw new ApplicationSourceNotFoundException($"there is no TextField within QuestionId (questionId:{questionId})");
            }

            return sections!;
        }
        private void ValidateAddDTO(AddTextFieldDTO dTO)
            => ValidateDTO(dTO.AnswerLength, dTO.TextFieldUIType);
        private void ValidateDTO(int? answerLength, int? textFieldUIType)
        {
            if (answerLength.HasValue && answerLength >= 1 && answerLength < 8000)
                throw new ApplicationValidationException("valid AnswerLength is from 1 to 8000");

            if (textFieldUIType.HasValue && Enum.IsDefined(typeof(TextFieldUIType), textFieldUIType))
                throw new ApplicationValidationException("TextFieldUIType is not valid");
        }
    }
}
