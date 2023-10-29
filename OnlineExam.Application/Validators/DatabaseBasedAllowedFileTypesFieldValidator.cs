using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.AllowedFileTypesFieldDTOs;
using OnlineExam.Application.Contract.Exceptions;

namespace OnlineExam.Application.Validators
{
    public class DatabaseBasedAllowedFileTypesFieldValidator : IDatabaseBasedAllowedFileTypesFieldValidator
    {
        readonly IAllowedFileTypesFieldInternalService _internalService;

        public DatabaseBasedAllowedFileTypesFieldValidator(IAllowedFileTypesFieldInternalService repository)
        {
            _internalService = repository;
        }

        public void DatabaseBasedValidate(AddAllowedFileTypesFieldDTO dTO)
            => DatabaseBasedValidateValues(dTO.Extension);

        public void DatabaseBasedValidate(int allowedFileTypesField, UpdateAllowedFileTypesFieldDTO dTO)
            => DatabaseBasedValidateValues(dTO.Extension, allowedFileTypesField);

        private void DatabaseBasedValidateValues(string extension, int? id = null)
        {
            if (_internalService.GetIQueryable()
                .Where(cfo => !id.HasValue
                    || cfo.Id != id.Value)
                .Any(e => e.Extension == extension))
                throw new ApplicationValidationException($"duplicate extension: an other FileType has {extension} as Extension");
        }
    }
}
