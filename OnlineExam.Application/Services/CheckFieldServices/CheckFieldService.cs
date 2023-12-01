using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.CheckFieldServices
{
    public sealed class CheckFieldService : ICheckFieldService
    {
        readonly ICheckFieldInternalService _checkFieldInternalService;
        readonly ICheckFieldMapper _checkFieldMapper;
        readonly ICheckFieldDTOValidator _checkFieldDTOValidator;
        readonly ICheckFieldAccessValidator _checkFieldAccessValidator;

        public CheckFieldService(ICheckFieldInternalService checkFieldInternalService,
                                 ICheckFieldMapper checkFieldMapper,
                                 ICheckFieldDTOValidator checkFieldValidator,
                                 ICheckFieldAccessValidator checkFieldAccessValidator)
        {
            _checkFieldInternalService = checkFieldInternalService;
            _checkFieldMapper = checkFieldMapper;
            _checkFieldDTOValidator = checkFieldValidator;
            _checkFieldAccessValidator = checkFieldAccessValidator;
        }

        public ShowCheckFieldDTO Add(int questionId, string issuerUserId, AddCheckFieldDTO checkField)
        {
            _checkFieldAccessValidator.ThrowIfUserIsNotExamCreator(questionId, issuerUserId);

            _checkFieldDTOValidator.ValidateDTO(checkField);

            var newCheckField = _checkFieldMapper.AddDTOToEntity(questionId, checkField)!;

            _checkFieldInternalService.Add(newCheckField);

            return _checkFieldMapper.EntityToShowDTO(newCheckField)!;
        }

        public void Delete(int checkFieldId, string issuerUserId)
        {
            var checkField = GetCheckFieldWith_Question_Section_Exam_Included(checkFieldId);

            _checkFieldAccessValidator.ThrowIfUserIsNotExamCreator(issuerUserId, checkField.Question.Section.Exam);

            _checkFieldInternalService.Delete(checkField);
        }

        public IEnumerable<ShowCheckFieldDTO> GetAllByQuestionId(int questionId, string issuerUserId, int skip = 0, int take = 20)
        {
            _checkFieldAccessValidator.ThrowIfUserIsNotExamCreatorOrExamUser(questionId, issuerUserId);

            return _checkFieldInternalService.GetAllByParentId(questionId, skip, take).Select(_checkFieldMapper.EntityToShowDTO);
        }

        public ShowCheckFieldDTO? GetById(int checkFieldId, string issuerUserId)
        {
            var checkField = GetCheckFieldWith_Question_Section_Exam_Included(checkFieldId);

            _checkFieldAccessValidator.ThrowIfUserIsNotExamCreatorOrExamUser(issuerUserId, checkField.Question.Section.Exam);

            return _checkFieldMapper.EntityToShowDTO(checkField);
        }

        public void Update(int checkFieldId, string issuerUserId, UpdateCheckFieldDTO dTO)
        {
            var checkField = GetCheckFieldWith_Question_Section_Exam_Included(checkFieldId);

            _checkFieldAccessValidator.ThrowIfUserIsNotExamCreator(issuerUserId, checkField.Question.Section.Exam);

            _checkFieldDTOValidator.ValidateDTO(dTO);

            _checkFieldMapper.UpdateEntityByDTO(checkField, dTO);

            _checkFieldInternalService.Update(checkField);
        }

        private CheckField GetCheckFieldWith_Question_Section_Exam_Included(int checkFieldId)
        {
            return _checkFieldInternalService.GetById(checkFieldId,
                _checkFieldInternalService.GetIQueryable()
                .Include(x => x.Question)
                .ThenInclude(x => x.Section)
                .ThenInclude(x => x.Exam));
        }
    }
}
