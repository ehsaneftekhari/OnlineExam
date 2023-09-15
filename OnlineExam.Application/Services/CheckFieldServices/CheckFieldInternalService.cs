using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Services.QuestionServices;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.CheckFieldServices
{
    public class CheckFieldInternalService
    {
        readonly ICheckFieldRepository _checkFieldRepository;
        readonly QuestionInternalService _questionInternalService;

        public CheckFieldInternalService(ICheckFieldRepository checkFieldRepository,
                                         QuestionInternalService questionInternalService)
        {
            _checkFieldRepository = checkFieldRepository;
            _questionInternalService = questionInternalService;
        }

        internal CheckField Add(CheckField checkField)
        {
            ThrowIfCheckFieldIsNotValid(checkField);

            _questionInternalService.ThrowIfQuestionIdIsNotValid(checkField.QuestionId);

            try
            {
                if (_checkFieldRepository.Add(checkField) > 0 && checkField.Id > 0)
                    return checkField;

                throw new Exception();
            }
            catch
            {
                _questionInternalService.ThrowExceptionIfQuestionIsNotExists(checkField.QuestionId);

                throw;
            }
        }

        internal void Delete(int checkFieldId)
        {
            if (_checkFieldRepository.Delete(GetById(checkFieldId)) < 0)
                throw new OEApplicationException("checkField did not Deleted");
        }

        internal IEnumerable<CheckField> GetAllByQuestionId(int questionId, int skip = 0, int take = 20)
        {
            _questionInternalService.ThrowIfQuestionIdIsNotValid(questionId);

            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var checkFields =
                _checkFieldRepository.GetIQueryable()
                .Where(q => q.QuestionId == questionId)
                .Skip(skip)
                .Take(take)
                .ToList();

            if (!checkFields.Any())
            {
                _questionInternalService.ThrowExceptionIfQuestionIsNotExists(questionId);

                throw new ApplicationSourceNotFoundException($"there is no CheckField within Question (questionId:{questionId})");
            }

            return checkFields!;
        }

        internal CheckField GetById(int checkFieldId)
        {
            ThrowIfCheckFieldIdIsNotValid(checkFieldId);

            var checkField = _checkFieldRepository.GetById(checkFieldId);

            if (checkField == null)
                throw new ApplicationSourceNotFoundException($"CheckField with id:{checkFieldId} is not exists");

            return checkField;
        }

        internal void Update(CheckField checkField)
        {
            ThrowIfCheckFieldIsNotValid(checkField);

            if (_checkFieldRepository.Update(checkField) <= 0)
                throw new OEApplicationException("CheckField did not updated");
        }

        internal void ThrowIfCheckFieldIdIsNotValid(int checkFieldId)
        {
            if (checkFieldId < 1)
                throw new ApplicationValidationException($"{nameof(checkFieldId)} can not be less than 1");
        }

        internal void ThrowIfCheckFieldIsNotValid(CheckField checkField)
        {
            if (checkField == null)
                throw new ArgumentNullException();
        }
    }
}
