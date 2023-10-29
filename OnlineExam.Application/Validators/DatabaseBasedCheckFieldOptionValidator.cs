using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class DatabaseBasedCheckFieldOptionValidator : IDatabaseBasedCheckFieldOptionValidator
    {
        readonly IBaseInternalService<CheckFieldOption, Question> _checkFieldOptionInternalService;

        public DatabaseBasedCheckFieldOptionValidator(IBaseInternalService<CheckFieldOption, Question> checkFieldOptionRepository)
        {
            _checkFieldOptionInternalService = checkFieldOptionRepository;
        }

        public void DatabaseBasedValidate(int checkFieldId, AddCheckFieldOptionDTO dTO)
            => DatabaseBasedValidateValues(checkFieldId, dTO.Order);

        public void DatabaseBasedValidate(int checkFieldId, int checkFieldOptionId, UpdateCheckFieldOptionDTO dTO)
            => DatabaseBasedValidateValues(checkFieldId, dTO.Order, checkFieldOptionId);

        private void DatabaseBasedValidateValues(int checkFieldId, int? order, int? checkFieldOptionId = null)
        {
            if (!order.HasValue)
                return;

            if (_checkFieldOptionInternalService.GetIQueryable()
                .Where(cfo => cfo.CheckFieldId == checkFieldId)
                .Where(cfo => !checkFieldOptionId.HasValue
                    || cfo.Id != checkFieldOptionId.Value)
                .Any(cfo => cfo.Order == order))
                throw new ApplicationValidationException($"duplicate order: an other checkFieldOption in checkField (checkFieldId: {checkFieldId}) has {order} order");
        }
    }
}
