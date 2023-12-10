using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.CheckFieldServices
{
    public sealed class CheckFieldOptionService : ICheckFieldOptionService
    {
        readonly ICheckFieldOptionInternalService _checkFieldOptionInternalService;
        readonly ICheckFieldOptionMapper _checkFieldOptionMapper;
        readonly ICheckFieldOptionDTOValidator _checkFieldOptionDTOValidator;
        readonly ICheckFieldOptionRelationValidator _heckFieldOptionRelationValidator;
        readonly ICheckFieldOptionAccessValidator _checkFieldOptionAccessValidator;

        public CheckFieldOptionService(ICheckFieldOptionInternalService checkFieldOptionInternalService,
                                       ICheckFieldOptionMapper checkFieldOptionMapper,
                                       ICheckFieldOptionDTOValidator checkFieldOptionValidator,
                                       ICheckFieldOptionRelationValidator databaseBasedCheckFieldOptionValidator,
                                       ICheckFieldOptionAccessValidator checkFieldOptionAccessValidator)
        {
            _checkFieldOptionInternalService = checkFieldOptionInternalService;
            _checkFieldOptionMapper = checkFieldOptionMapper;
            _checkFieldOptionDTOValidator = checkFieldOptionValidator;
            _heckFieldOptionRelationValidator = databaseBasedCheckFieldOptionValidator;
            _checkFieldOptionAccessValidator = checkFieldOptionAccessValidator;
        }

        public ShowCheckFieldOptionDTO Add(int checkFieldId, string issuerUserId, AddCheckFieldOptionDTO CheckFieldOption)
        {
            _checkFieldOptionAccessValidator.ThrowIfUserIsNotExamCreator(checkFieldId, issuerUserId);

            _checkFieldOptionDTOValidator.ValidateDTO(CheckFieldOption);

            _heckFieldOptionRelationValidator.DatabaseBasedValidate(checkFieldId, CheckFieldOption);

            var checkFieldOption = _checkFieldOptionMapper.AddDTOToEntity(checkFieldId, CheckFieldOption)!;

            _checkFieldOptionInternalService.Add(checkFieldOption);

            return _checkFieldOptionMapper.EntityToShowDTO(checkFieldOption)!;
        }

        public void Delete(int CheckFieldOptionId, string issuerUserId)
        {
            var checkFieldOption = GetCheckFieldOptionWith_CheckField_Question_Section_Exam_Included(CheckFieldOptionId);

            _checkFieldOptionAccessValidator.ThrowIfUserIsNotExamCreator(issuerUserId, checkFieldOption.CheckField.Question.Section.Exam);

            _checkFieldOptionInternalService.Delete(checkFieldOption);
        }

        public IEnumerable<ShowCheckFieldOptionDTO> GetAllByCheckFieldId(int checkFieldId, string issuerUserId, int skip = 0, int take = 20)
        {
            _checkFieldOptionAccessValidator.ThrowIfUserIsNotExamCreatorOrExamUser(checkFieldId, issuerUserId);

            return _checkFieldOptionInternalService.GetAllByParentId(checkFieldId, skip, take).Select(_checkFieldOptionMapper.EntityToShowDTO);
        }

        public ShowCheckFieldOptionDTO? GetById(int checkFieldOptionId, string issuerUserId)
        {
            var checkFieldOption = GetCheckFieldOptionWith_CheckField_Question_Section_Exam_Included(checkFieldOptionId);

            _checkFieldOptionAccessValidator.ThrowIfUserIsNotExamCreatorOrExamUser(issuerUserId, checkFieldOption.CheckField.Question.Section.Exam);

            return _checkFieldOptionMapper.EntityToShowDTO(_checkFieldOptionInternalService.GetById(checkFieldOptionId));
        }

        public void Update(int checkFieldOptionId, string issuerUserId, UpdateCheckFieldOptionDTO dTO)
        {
            var checkFieldOption = GetCheckFieldOptionWith_CheckField_Question_Section_Exam_Included(checkFieldOptionId);

            _checkFieldOptionAccessValidator.ThrowIfUserIsNotExamCreator(issuerUserId, checkFieldOption.CheckField.Question.Section.Exam);

            _checkFieldOptionDTOValidator.ValidateDTO(dTO);

            _heckFieldOptionRelationValidator.DatabaseBasedValidate(checkFieldOption.CheckFieldId, checkFieldOptionId, dTO);

            _checkFieldOptionMapper.UpdateEntityByDTO(checkFieldOption, dTO);

            _checkFieldOptionInternalService.Update(checkFieldOption);
        }

        private CheckFieldOption GetCheckFieldOptionWith_CheckField_Question_Section_Exam_Included(int checkFieldId)
        {
            return _checkFieldOptionInternalService.GetById(checkFieldId,
                _checkFieldOptionInternalService.GetIQueryable()
                .Include(x => x.CheckField)
                .ThenInclude(x => x.Question)
                .ThenInclude(x => x.Section)
                .ThenInclude(x => x.Exam));
        }
    }
}
