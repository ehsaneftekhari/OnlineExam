using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.AllowedFileTypesFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Infrastructure.Contract.IRepositories;

namespace OnlineExam.Application.Services
{
    public class AllowedFileTypesFieldService : IAllowedFileTypesFieldService
    {
        readonly IAllowedFileTypesFieldOptionRepository _repository;
        readonly IAllowedFileTypesFieldMapper _mapper;
        readonly IAllowedFileTypesFieldValidator _validator;
        readonly IDatabaseBasedAllowedFileTypesFieldValidator _databaseBasedValidator;

        public AllowedFileTypesFieldService(
            IAllowedFileTypesFieldOptionRepository repository
            ,IAllowedFileTypesFieldMapper mapper
            ,IAllowedFileTypesFieldValidator validator
            ,IDatabaseBasedAllowedFileTypesFieldValidator databaseBasedValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
            _databaseBasedValidator = databaseBasedValidator;
        }

        public ShowAllowedFileTypesFieldDTO Add(AddAllowedFileTypesFieldDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            _validator.ValidateDTO(dTO);

            try
            {
                var allowedFileTypesField = _mapper.AddDTOToEntity(dTO)!;
                if (_repository.Add(allowedFileTypesField) > 0 && allowedFileTypesField.Id > 0)
                    return _mapper.EntityToShowDTO(allowedFileTypesField)!;

                throw new Exception();
            }
            catch
            {
                throw new OEApplicationException($"AllowedFileTypesField did not added");
            }
        }

        public void Delete(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var allowedFileTypesField = _repository.GetById(id);

            if (allowedFileTypesField == null)
                throw new ApplicationSourceNotFoundException($"AllowedFileTypesField with id:{id} is not exists");

            if (_repository.Delete(allowedFileTypesField) < 0)
                throw new Exception();
        }

        public IEnumerable<ShowAllowedFileTypesFieldDTO> GetAll(int skip = 0, int take = 20)
        {
            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var allowedFileTypesFields =
                _repository.GetIQueryable()
                .Skip(skip)
                .Take(take)
                .ToList()
                .Select(_mapper.EntityToShowDTO);

            if (!allowedFileTypesFields.Any())
                throw new ApplicationSourceNotFoundException($"there is no AllowedFileTypesField");
            
            return allowedFileTypesFields!;
        }

        public ShowAllowedFileTypesFieldDTO? GetById(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var allowedFileTypesField = _repository.GetById(id);

            if (allowedFileTypesField == null)
                throw new ApplicationSourceNotFoundException($"AllowedFileTypesField with id:{id} is not exists");

            return _mapper.EntityToShowDTO(allowedFileTypesField);
        }

        public void Update(int id, UpdateAllowedFileTypesFieldDTO dTO)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            if (dTO == null)
                throw new ArgumentNullException();

            _validator.ValidateDTO(dTO);

            var allowedFileTypesField = _repository.GetById(id);

            if (allowedFileTypesField == null)
                throw new ApplicationSourceNotFoundException($"CheckFieldOption with id:{id} is not exists");

            _databaseBasedValidator.DatabaseBasedValidate(id, dTO);

            _mapper.UpdateEntityByDTO(allowedFileTypesField, dTO);

            if (_repository.Update(allowedFileTypesField) <= 0)
                throw new Exception();
        }
    }
}
