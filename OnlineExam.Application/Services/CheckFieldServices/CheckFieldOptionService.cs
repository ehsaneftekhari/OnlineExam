using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Validators;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.CheckFieldServices
{
    public sealed class CheckFieldOptionService : ICheckFieldOptionService
    {
        readonly ICheckFieldOptionInternalService _checkFieldOptionInternalService;
        readonly ICheckFieldOptionMapper _checkFieldOptionMapper;
        readonly ICheckFieldOptionValidator _checkFieldOptionValidator;
        readonly IDatabaseBasedCheckFieldOptionValidator _databaseBasedCheckFieldOptionValidator;

        public CheckFieldOptionService(ICheckFieldOptionInternalService checkFieldOptionInternalService,
                                       ICheckFieldOptionMapper checkFieldOptionMapper,
                                       ICheckFieldOptionValidator checkFieldOptionValidator,
                                       IDatabaseBasedCheckFieldOptionValidator databaseBasedCheckFieldOptionValidator)
        {
            _checkFieldOptionInternalService = checkFieldOptionInternalService;
            _checkFieldOptionMapper = checkFieldOptionMapper;
            _checkFieldOptionValidator = checkFieldOptionValidator;
            _databaseBasedCheckFieldOptionValidator = databaseBasedCheckFieldOptionValidator;
        }

        public ShowCheckFieldOptionDTO Add(int checkFieldId, string issuerUserId, AddCheckFieldOptionDTO CheckFieldOption)
        {
            _checkFieldOptionValidator.ThrowIfUserIsNotExamCreator(checkFieldId, issuerUserId);

            _checkFieldOptionValidator.ValidateDTO(CheckFieldOption);

            _databaseBasedCheckFieldOptionValidator.DatabaseBasedValidate(checkFieldId, CheckFieldOption);

            var checkFieldOption = _checkFieldOptionMapper.AddDTOToEntity(checkFieldId, CheckFieldOption)!;

            _checkFieldOptionInternalService.Add(checkFieldOption);

            return _checkFieldOptionMapper.EntityToShowDTO(checkFieldOption)!;
        }

        public void Delete(int CheckFieldOptionId, string issuerUserId)
        {
            var checkFieldOption = GetCheckFieldOptionWith_CheckField_Question_Section_Exam_Included(CheckFieldOptionId);

            _checkFieldOptionValidator.ThrowIfUserIsNotExamCreator(issuerUserId, checkFieldOption.CheckField.Question.Section.Exam);

            _checkFieldOptionInternalService.Delete(checkFieldOption);
        }

        public IEnumerable<ShowCheckFieldOptionDTO> GetAllByCheckFieldId(int checkFieldId, string issuerUserId, int skip = 0, int take = 20)
        {
            _checkFieldOptionValidator.ThrowIfUserIsNotExamCreatorOrExamUser(checkFieldId, issuerUserId);

            return _checkFieldOptionInternalService.GetAllByParentId(checkFieldId, skip, take).Select(_checkFieldOptionMapper.EntityToShowDTO);
        }

        public ShowCheckFieldOptionDTO? GetById(int checkFieldOptionId, string issuerUserId)
        {
            var checkFieldOption = GetCheckFieldOptionWith_CheckField_Question_Section_Exam_Included(checkFieldOptionId);

            _checkFieldOptionValidator.ThrowIfUserIsNotExamCreatorOrExamUser(issuerUserId, checkFieldOption.CheckField.Question.Section.Exam);

            return _checkFieldOptionMapper.EntityToShowDTO(_checkFieldOptionInternalService.GetById(checkFieldOptionId));
        }

        public void Update(int checkFieldOptionId, string issuerUserId, UpdateCheckFieldOptionDTO dTO)
        {
            var checkFieldOption = GetCheckFieldOptionWith_CheckField_Question_Section_Exam_Included(checkFieldOptionId);

            _checkFieldOptionValidator.ThrowIfUserIsNotExamCreator(issuerUserId, checkFieldOption.CheckField.Question.Section.Exam);

            _checkFieldOptionValidator.ValidateDTO(dTO);

            _databaseBasedCheckFieldOptionValidator.DatabaseBasedValidate(checkFieldOption.CheckFieldId, checkFieldOptionId, dTO);

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
