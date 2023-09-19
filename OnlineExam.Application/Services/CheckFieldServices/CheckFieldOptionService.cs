using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.IServices;

namespace OnlineExam.Application.Services.CheckFieldServices
{
    public class CheckFieldOptionService : ICheckFieldOptionService
    {
        readonly CheckFieldOptionInternalService _checkFieldOptionInternalService;
        readonly ICheckFieldOptionMapper _checkFieldOptionMapper;
        readonly ICheckFieldOptionValidator _checkFieldOptionValidator;
        readonly IDatabaseBasedCheckFieldOptionValidator _databaseBasedCheckFieldOptionValidator;

        public CheckFieldOptionService(CheckFieldOptionInternalService checkFieldOptionInternalService,
                                       ICheckFieldOptionMapper checkFieldOptionMapper,
                                       ICheckFieldOptionValidator checkFieldOptionValidator,
                                       IDatabaseBasedCheckFieldOptionValidator databaseBasedCheckFieldOptionValidator)
        {
            _checkFieldOptionInternalService = checkFieldOptionInternalService;
            _checkFieldOptionMapper = checkFieldOptionMapper;
            _checkFieldOptionValidator = checkFieldOptionValidator;
            _databaseBasedCheckFieldOptionValidator = databaseBasedCheckFieldOptionValidator;
        }

        public ShowCheckFieldOptionDTO Add(int checkFieldId, AddCheckFieldOptionDTO CheckFieldOption)
        {
            _checkFieldOptionValidator.ValidateDTO(CheckFieldOption);

            _databaseBasedCheckFieldOptionValidator.DatabaseBasedValidate(checkFieldId, CheckFieldOption);

            var checkField = _checkFieldOptionMapper.AddDTOToEntity(checkFieldId, CheckFieldOption)!;

            _checkFieldOptionInternalService.Add(checkField);

            return _checkFieldOptionMapper.EntityToShowDTO(checkField)!;
        }

        public void Delete(int CheckFieldOptionId)
            => _checkFieldOptionInternalService.Delete(CheckFieldOptionId);

        public IEnumerable<ShowCheckFieldOptionDTO> GetAllByCheckFieldId(int checkFieldId, int skip = 0, int take = 20)
            => _checkFieldOptionInternalService.GetAllByParentId(checkFieldId, skip, take).Select(_checkFieldOptionMapper.EntityToShowDTO);

        public ShowCheckFieldOptionDTO? GetById(int CheckFieldOptionId)
            => _checkFieldOptionMapper.EntityToShowDTO(_checkFieldOptionInternalService.GetById(CheckFieldOptionId));

        public void Update(int id, UpdateCheckFieldOptionDTO dTO)
        {
            _checkFieldOptionValidator.ValidateDTO(dTO);

            var checkFieldOption = _checkFieldOptionInternalService.GetById(id);

            _databaseBasedCheckFieldOptionValidator.DatabaseBasedValidate(checkFieldOption.CheckFieldId, id, dTO);

            _checkFieldOptionMapper.UpdateEntityByDTO(checkFieldOption, dTO);

            _checkFieldOptionInternalService.Update(checkFieldOption);
        }
    }
}
