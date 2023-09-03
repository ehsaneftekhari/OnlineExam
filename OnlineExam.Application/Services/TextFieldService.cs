using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.IMappers;
using OnlineExam.Infrastructure.Contract.IRepositories;

namespace OnlineExam.Application.Services
{
    public class TextFieldService : ITextFieldService
    {
        readonly ITextFieldMapper _textFieldMapper;
        readonly ITextFieldRepository _textFieldRepository;

        public TextFieldService(ITextFieldRepository textFieldRepository)
        {
            _textFieldRepository = textFieldRepository;
        }

        public ShowTextFieldDTO Add(int questionId, AddTextFieldDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            if (questionId < 1)
                throw new ApplicationValidationException("QuestionId can not be less than 1");

            try
            {
                var newTextField = _textFieldMapper.AddDTOToEntity(questionId, dTO)!;
                if (_textFieldRepository.Add(newTextField) > 0 && newTextField.Id > 0)
                    return _textFieldMapper.EntityToShowDTO(newTextField)!;

                throw new Exception();
            }
            catch
            {
                if (_textFieldRepository.GetById(questionId) == null)
                    throw new OEApplicationException($"QuestionId with id:{questionId} is not exists");

                throw;
            }
        }
    }
}
