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
        readonly ITextFieldValidator _textFieldValidator;

        public TextFieldService(ITextFieldInternalService textFieldInternalService, ITextFieldMapper textFieldMapper, ITextFieldValidator textFieldValidator)
        {
            _textFieldInternalService = textFieldInternalService;
            _textFieldMapper = textFieldMapper;
            _textFieldValidator = textFieldValidator;
        }

        public ShowTextFieldDTO Add(int questionId, AddTextFieldDTO dTO)
        {
            _textFieldValidator.ValidateDTO(dTO);
            var newTextField = _textFieldMapper.AddDTOToEntity(questionId, dTO)!;
            _textFieldInternalService.Add(newTextField);
            return _textFieldMapper.EntityToShowDTO(newTextField)!;
        }

        public ShowTextFieldDTO? GetById(int textFieldId)
             => _textFieldMapper.EntityToShowDTO(_textFieldInternalService.GetById(textFieldId));

        public IEnumerable<ShowTextFieldDTO> GetAllByQuestionId(int questionId, int skip = 0, int take = 20)
            => _textFieldInternalService.GetAllByParentId(questionId, skip, take).Select(_textFieldMapper.EntityToShowDTO);

        public void Update(int textFieldId, UpdateTextFieldDTO dTO)
        {
            _textFieldValidator.ValidateDTO(dTO);

            var textField = _textFieldInternalService.GetById(textFieldId);

            _textFieldMapper.UpdateEntityByDTO(textField, dTO);

            _textFieldInternalService.Update(textField);
        }

        public void Delete(int textFieldId)
            => _textFieldInternalService.Delete(textFieldId);
    }
}
