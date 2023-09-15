using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Services.QuestionServices;
using OnlineExam.Infrastructure.Contract.IRepositories;

namespace OnlineExam.Application.Services.CheckFieldServices
{
    public class CheckFieldService : ICheckFieldService
    {
        readonly CheckFieldInternalService _checkFieldInternalService;
        readonly QuestionInternalService _questionInternalService;
        readonly ICheckFieldMapper _checkFieldMapper;
        readonly ICheckFieldValidator _checkFieldValidator;

        public CheckFieldService(CheckFieldInternalService checkFieldInternalService,
                                 QuestionInternalService questionInternalService,
                                 ICheckFieldMapper checkFieldMapper,
                                 ICheckFieldValidator checkFieldValidator)
        {
            _checkFieldInternalService = checkFieldInternalService;
            _questionInternalService = questionInternalService;
            _checkFieldMapper = checkFieldMapper;
            _checkFieldValidator = checkFieldValidator;
        }

        public ShowCheckFieldDTO Add(int questionId, AddCheckFieldDTO checkField)
        {
            _checkFieldValidator.ValidateDTO(checkField);

            var newCheckField = _checkFieldMapper.AddDTOToEntity(questionId, checkField)!;

            _checkFieldInternalService.Add(newCheckField);

            return _checkFieldMapper.EntityToShowDTO(newCheckField)!;
        }

        public void Delete(int checkFieldId)
            => _checkFieldInternalService.Delete(checkFieldId);

        public IEnumerable<ShowCheckFieldDTO> GetAllByQuestionId(int questionId, int skip = 0, int take = 20)
            => _checkFieldInternalService.GetAllByQuestionId(questionId, skip, take).Select(_checkFieldMapper.EntityToShowDTO);

        public ShowCheckFieldDTO? GetById(int checkFieldId)
            => _checkFieldMapper.EntityToShowDTO(_checkFieldInternalService.GetById(checkFieldId));

        public void Update(int checkFieldId, UpdateCheckFieldDTO dTO)
        {
            _checkFieldValidator.ValidateDTO(dTO);

            var checkField = _checkFieldInternalService.GetById(checkFieldId);

            _checkFieldMapper.UpdateEntityByDTO(checkField, dTO);

            _checkFieldInternalService.Update(checkField);
        }
    }
}
