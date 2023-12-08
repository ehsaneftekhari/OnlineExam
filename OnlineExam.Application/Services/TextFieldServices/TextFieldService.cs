using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.TextFieldDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.TextFieldServices
{
    public sealed class TextFieldService : ITextFieldService
    {
        readonly ITextFieldInternalService _textFieldInternalService;
        readonly ITextFieldMapper _textFieldMapper;
        readonly ITextFieldDTOValidator _textFieldDTOValidator;
        readonly IQuestionComponentAccessValidator _questionComponentAccessValidator;

        public TextFieldService(ITextFieldInternalService textFieldInternalService,
                                ITextFieldMapper textFieldMapper,
                                ITextFieldDTOValidator textFieldDTOValidator,
                                IQuestionComponentAccessValidator questionComponentAccessValidator)
        {
            _textFieldInternalService = textFieldInternalService;
            _textFieldMapper = textFieldMapper;
            _textFieldDTOValidator = textFieldDTOValidator;
            _questionComponentAccessValidator = questionComponentAccessValidator;
        }

        public ShowTextFieldDTO Add(int questionId, string issuerUserId, AddTextFieldDTO dTO)
        {
            _questionComponentAccessValidator.ThrowIfUserIsNotExamCreator(questionId, issuerUserId);
            _textFieldDTOValidator.ValidateDTO(dTO);

            var newTextField = _textFieldMapper.AddDTOToEntity(questionId, dTO)!;
            _textFieldInternalService.Add(newTextField);
            return _textFieldMapper.EntityToShowDTO(newTextField)!;
        }

        public ShowTextFieldDTO? GetById(int textFieldId, string issuerUserId)
        {
            var textField = GetTextFieldWith_Question_Section_Exam_Included(textFieldId);

            _questionComponentAccessValidator.ThrowIfUserIsNotExamCreatorOrExamUser(issuerUserId, textField.Question.Section.Exam);

            return _textFieldMapper.EntityToShowDTO(textField);
        }

        public IEnumerable<ShowTextFieldDTO> GetAllByQuestionId(int questionId, string issuerUserId, int skip = 0, int take = 20)
        {
            _questionComponentAccessValidator.ThrowIfUserIsNotExamCreatorOrExamUser(questionId, issuerUserId);

            return _textFieldInternalService.GetAllByParentId(questionId, skip, take).Select(_textFieldMapper.EntityToShowDTO);
        }

        public void Update(int textFieldId, string issuerUserId, UpdateTextFieldDTO dTO)
        {
            var textField = GetTextFieldWith_Question_Section_Exam_Included(textFieldId);

            _questionComponentAccessValidator.ThrowIfUserIsNotExamCreator(issuerUserId, textField.Question.Section.Exam);

            _textFieldDTOValidator.ValidateDTO(dTO);

            _textFieldMapper.UpdateEntityByDTO(textField, dTO);

            _textFieldInternalService.Update(textField);
        }

        public void Delete(int textFieldId, string issuerUserId)
        {
            var textField = GetTextFieldWith_Question_Section_Exam_Included(textFieldId);

            _questionComponentAccessValidator.ThrowIfUserIsNotExamCreator(issuerUserId, textField.Question.Section.Exam);

            _textFieldInternalService.Delete(textFieldId);
        }

        private TextField GetTextFieldWith_Question_Section_Exam_Included(int textFieldId)
        {
            return _textFieldInternalService.GetById(textFieldId,
                _textFieldInternalService.GetIQueryable()
                .Include(x => x.Question)
                .ThenInclude(x => x.Section)
                .ThenInclude(x => x.Exam));
        }
    }
}
