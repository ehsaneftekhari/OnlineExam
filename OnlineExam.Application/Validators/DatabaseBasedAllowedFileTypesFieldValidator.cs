using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.AllowedFileTypesFieldDTOs;
using OnlineExam.Application.Contract.DTOs.CheckFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class DatabaseBasedAllowedFileTypesFieldValidator : IDatabaseBasedAllowedFileTypesFieldValidator
    {
        readonly IAllowedFileTypesFieldOptionRepository _repository;

        public DatabaseBasedAllowedFileTypesFieldValidator(IAllowedFileTypesFieldOptionRepository repository)
        {
            _repository = repository;
        }

        public void DatabaseBasedValidate(AddAllowedFileTypesFieldDTO dTO)
            => DatabaseBasedValidateValues(dTO.Extension);

        public void DatabaseBasedValidate(int allowedFileTypesField, UpdateAllowedFileTypesFieldDTO dTO)
            => DatabaseBasedValidateValues(dTO.Extension, allowedFileTypesField);

        private void DatabaseBasedValidateValues(string extension, int? id = null)
        {
            if (_repository.GetIQueryable()
                .Where(cfo => !id.HasValue
                    || cfo.Id != id.Value)
                .Any(e => e.Extension == extension))
                throw new ApplicationValidationException($"duplicate name: an other FileType has {extension} as Extension");
        }
    }
}
