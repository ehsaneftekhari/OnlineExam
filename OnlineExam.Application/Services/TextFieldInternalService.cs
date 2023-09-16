using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Services.QuestionServices;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services
{
    internal class TextFieldInternalService
    {
        readonly ITextFieldRepository _textFieldRepository;
        readonly QuestionInternalService _questionInternalService;

        public TextField Add(TextField textField)
        {
            ThrowIfTextFieldIsNotValid(textField);

            try
            {
                if (_textFieldRepository.Add(textField) > 0 && textField.Id > 0)
                    return textField;

                throw new OEApplicationException("TextField did not Added");
            }
            catch
            {
                _questionInternalService.ThrowExceptionIfQuestionIsNotExists(textField.QuestionId);

                throw;
            }
        }

        public TextField GetById(int textFieldId)
        {
            ThrowIfTextFieldIdIsNotValid(textFieldId);

            var textField = _textFieldRepository.GetById(textFieldId);

            if (textField == null)
                throw new ApplicationSourceNotFoundException($"TextField with id:{textFieldId} is not exists");

            return textField;
        }

        internal void ThrowExceptionIfTextFieldIsNotExists(int textFieldId) => GetById(textFieldId);

        public IEnumerable<TextField> GetAllByQuestionId(int questionId, int skip = 0, int take = 20)
        {
            _questionInternalService.ThrowIfQuestionIdIsNotValid(questionId);

            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var textFields =
                _textFieldRepository.GetIQueryable()
                .Where(q => q.QuestionId == questionId)
                .Skip(skip)
                .Take(take)
                .ToList();

            if (!textFields.Any())
            {
                _questionInternalService.ThrowExceptionIfQuestionIsNotExists(questionId);

                throw new ApplicationSourceNotFoundException($"there is no TextField within Question (questionId:{questionId})");
            }

            return textFields!;
        }

        public void Update(TextField textField)
        {
            ThrowIfTextFieldIsNotValid(textField);

            if (_textFieldRepository.Update(textField) <= 0)
                throw new OEApplicationException("TextField did not updated");
        }

        public void Delete(int textFieldId)
        {
            var textField = GetById(textFieldId);

            if (_textFieldRepository.Delete(textField) < 0)
                throw new OEApplicationException("TextField did not Deleted");
        }

        internal void ThrowIfTextFieldIdIsNotValid(int textFieldId)
        {
            if (textFieldId < 1)
                throw new ApplicationValidationException($"{nameof(textFieldId)} can not be less than 1");
        }

        internal void ThrowIfTextFieldIsNotValid(TextField textField)
        {
            if (textField == null)
                throw new ArgumentNullException();

            _questionInternalService.ThrowIfQuestionIdIsNotValid(textField.QuestionId);
        }
    }
}
