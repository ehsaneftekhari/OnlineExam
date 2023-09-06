using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Mappers;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services
{
    public class CheckFieldOptionService : ICheckFieldOptionService
    {
        readonly ICheckFieldOptionRepository _checkFieldOptionRepository;
        readonly ICheckFieldRepository _checkFieldRepository;
        readonly ICheckFieldOptionMapper _checkFieldOptionMapper;

        public CheckFieldOptionService(ICheckFieldOptionRepository checkFieldOptionRepository, ICheckFieldRepository checkFieldRepository, ICheckFieldOptionMapper checkFieldOptionMapper)
        {
            _checkFieldOptionRepository = checkFieldOptionRepository;
            _checkFieldRepository = checkFieldRepository;
            _checkFieldOptionMapper = checkFieldOptionMapper;
        }

        public ShowCheckFieldOptionDTO Add(int checkFieldId, AddCheckFieldOptionDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            if (checkFieldId < 1)
                throw new ApplicationValidationException("QuestionId can not be less than 1");

            ValidateDTO(dTO);

            DatabaseBasedValidate(checkFieldId, dTO);

            try
            {
                var newTextField = _checkFieldOptionMapper.AddDTOToEntity(checkFieldId, dTO)!;
                if (_checkFieldOptionRepository.Add(newTextField) > 0 && newTextField.Id > 0)
                    return _checkFieldOptionMapper.EntityToShowDTO(newTextField)!;

                throw new Exception();
            }
            catch
            {
                if (_checkFieldRepository.GetById(checkFieldId) == null)
                    throw new OEApplicationException($"CheckField with id:{checkFieldId} is not exists");

                throw;
            }
        }

        public void Delete(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var textFieldOption = _checkFieldOptionRepository.GetById(id);

            if (textFieldOption == null)
                throw new ApplicationSourceNotFoundException($"textFieldOption with id:{id} is not exists");

            if (_checkFieldOptionRepository.Delete(textFieldOption) < 0)
                throw new Exception();
        }

        public IEnumerable<ShowCheckFieldOptionDTO> GetAllByCheckFieldId(int checkFieldId, int skip = 0, int take = 20)
        {
            if (checkFieldId < 1)
                throw new ApplicationValidationException("checkFieldId can not be less than 1");

            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var checkFieldOptions =
                _checkFieldOptionRepository.GetIQueryable()
                .Where(q => q.CheckFieldId == checkFieldId)
                .Skip(skip)
                .Take(take)
                .OrderBy(q => q.Order)
                .ToList()
                .Select(_checkFieldOptionMapper.EntityToShowDTO);

            if (!checkFieldOptions.Any())
            {
                if (_checkFieldRepository.GetById(checkFieldId) == null)
                    throw new ApplicationSourceNotFoundException($"CheckField with id:{checkFieldId} is not exists");

                throw new ApplicationSourceNotFoundException($"there is no CheckFieldOption within checkField (checkFieldId:{checkFieldId})");
            }

            return checkFieldOptions!;
        }

        public ShowCheckFieldOptionDTO? GetById(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var checkFieldOption = _checkFieldOptionRepository.GetById(id);

            if (checkFieldOption == null)
                throw new ApplicationSourceNotFoundException($"CheckFieldOption with id:{id} is not exists");

            return _checkFieldOptionMapper.EntityToShowDTO(checkFieldOption);
        }

        public void Update(int id, UpdateCheckFieldOptionDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            ValidateDTO(dTO);

            var checkFieldOption = _checkFieldOptionRepository.GetById(id);

            if (checkFieldOption == null)
                throw new ApplicationSourceNotFoundException($"CheckFieldOption with id:{id} is not exists");

            DatabaseBasedValidate(checkFieldOption.CheckFieldId, id, dTO);

            _checkFieldOptionMapper.UpdateEntityByDTO(checkFieldOption, dTO);

            if (_checkFieldOptionRepository.Update(checkFieldOption) <= 0)
                throw new Exception();
        }

        private void ValidateDTO(AddCheckFieldOptionDTO dTO)
            => ValidateValues(dTO.Order, dTO.Text);

        private void ValidateDTO(UpdateCheckFieldOptionDTO dTO)
            => ValidateValues(dTO.Order, dTO.Text);

        private void ValidateValues(int? Order, string? Text)
        {
            if (Order.HasValue && (Order < 1))
                throw new ApplicationValidationException("Order can not be less then 1");

            if (Text != null && (Text!.Length > 4000))
                throw new ApplicationValidationException("Text length can not be more than 4000 characters");
        }

        private void DatabaseBasedValidate(int checkFieldId, AddCheckFieldOptionDTO dTO)
            => DatabaseBasedValidateValues(checkFieldId, dTO.Order);


        private void DatabaseBasedValidate(int checkFieldId, int checkFieldOptionId, UpdateCheckFieldOptionDTO dTO)
            => DatabaseBasedValidateValues(checkFieldId, dTO.Order, checkFieldOptionId);

        private void DatabaseBasedValidateValues(int checkFieldId, int? order, int? checkFieldOptionId = null)
        {
            if (!order.HasValue)
                return;

            if (_checkFieldOptionRepository.GetIQueryable()
                .Where(cfo => cfo.CheckFieldId == checkFieldId)
                .Where(cfo => !checkFieldOptionId.HasValue 
                    || cfo.Id != checkFieldOptionId.Value)
                .Any(cfo => cfo.Order == order))
                throw new ApplicationValidationException($"duplicate order: an other checkFieldOption in checkField (checkFieldId: {checkFieldId}) has {order} order");
        }
    }
}
