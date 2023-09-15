using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.CheckFieldServices
{
    public class CheckFieldOptionInternalService
    {
        readonly ICheckFieldOptionRepository _checkFieldOptionRepository;
        readonly CheckFieldInternalService _checkFieldInternalService;

        public CheckFieldOptionInternalService(ICheckFieldOptionRepository checkFieldOptionRepository,
                                               CheckFieldInternalService checkFieldInternalService)
        {
            _checkFieldOptionRepository = checkFieldOptionRepository;
            _checkFieldInternalService = checkFieldInternalService;
        }

        internal CheckFieldOption Add(CheckFieldOption checkFieldOption)
        {
            ThrowIfCheckFieldOptionIsNotValid(checkFieldOption);

            _checkFieldInternalService.ThrowIfCheckFieldIdIsNotValid(checkFieldOption.CheckFieldId);

            try
            {
                if (_checkFieldOptionRepository.Add(checkFieldOption) > 0 && checkFieldOption.Id > 0)
                    return checkFieldOption;

                throw new Exception();
            }
            catch
            {
                _checkFieldInternalService.ThrowExceptionIfCheckFieldIsNotExists(checkFieldOption.CheckFieldId);

                throw;
            }
        }

        internal void Delete(int checkFieldOptionId)
        {
            if (_checkFieldOptionRepository.Delete(GetById(checkFieldOptionId)) < 0)
                throw new OEApplicationException("CheckFieldOption did not Deleted");
        }

        internal IEnumerable<CheckFieldOption> GetAllByCheckFieldId(int checkFieldId, int skip = 0, int take = 20)
        {
            _checkFieldInternalService.ThrowIfCheckFieldIdIsNotValid(checkFieldId);

            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var checkFieldOptions =
                _checkFieldOptionRepository.GetIQueryable()
                .Where(q => q.CheckFieldId == checkFieldId)
                .Skip(skip)
                .Take(take)
                .OrderBy(q => q.Order)
                .ToList();

            if (!checkFieldOptions.Any())
            {
                _checkFieldInternalService.ThrowExceptionIfCheckFieldIsNotExists(checkFieldId);

                throw new ApplicationSourceNotFoundException($"there is no CheckFieldOption within checkField (checkFieldId:{checkFieldId})");
            }

            return checkFieldOptions!;
        }

        internal CheckFieldOption GetById(int checkFieldOptionId)
        {
            ThrowIfCheckFieldOptionIsIdNotValid(checkFieldOptionId);

            var checkFieldOption = _checkFieldOptionRepository.GetById(checkFieldOptionId);

            if (checkFieldOption == null)
                throw new ApplicationSourceNotFoundException($"CheckFieldOption with id:{checkFieldOptionId} is not exists");

            return checkFieldOption;
        }

        internal void Update(CheckFieldOption checkFieldOption)
        {
            ThrowIfCheckFieldOptionIsNotValid(checkFieldOption);

            if (_checkFieldOptionRepository.Update(checkFieldOption) <= 0)
                throw new OEApplicationException("CheckFieldOption did not Updated");
        }

        internal void ThrowIfCheckFieldOptionIsIdNotValid(int checkFieldOptionId)
        {
            if (checkFieldOptionId < 1)
                throw new ApplicationValidationException($"{nameof(checkFieldOptionId)} can not be less than 1");
        }

        internal void ThrowIfCheckFieldOptionIsNotValid(CheckFieldOption checkFieldOption)
        {
            if (checkFieldOption == null)
                throw new ArgumentNullException();
        }
    }
}
