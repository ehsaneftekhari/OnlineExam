﻿using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.AllowedFileTypesFieldDTOs;
using OnlineExam.Application.Contract.IServices;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.FileFieldServices
{
    public sealed class AllowedFileTypesFieldService : IAllowedFileTypesFieldService
    {
        readonly AllowedFileTypesFieldInternalService _internalService;
        readonly IAllowedFileTypesFieldMapper _mapper;
        readonly IAllowedFileTypesFieldValidator _validator;
        readonly IDatabaseBasedAllowedFileTypesFieldValidator _databaseBasedValidator;

        public AllowedFileTypesFieldService(AllowedFileTypesFieldInternalService internalService,
                                            IAllowedFileTypesFieldMapper mapper,
                                            IAllowedFileTypesFieldValidator validator,
                                            IDatabaseBasedAllowedFileTypesFieldValidator databaseBasedValidator)
        {
            _internalService = internalService;
            _mapper = mapper;
            _validator = validator;
            _databaseBasedValidator = databaseBasedValidator;
        }

        public ShowAllowedFileTypesFieldDTO Add(AddAllowedFileTypesFieldDTO dTO)
        {
            _validator.ValidateDTO(dTO);
            _databaseBasedValidator.DatabaseBasedValidate(dTO);
            var allowedFileTypesField = _mapper.AddDTOToEntity(dTO)!;
            _internalService.Add(allowedFileTypesField);
             return _mapper.EntityToShowDTO(allowedFileTypesField)!;
        }

        public void Delete(int allowedFileTypeId)
            => _internalService.Delete(allowedFileTypeId);

        public IEnumerable<ShowAllowedFileTypesFieldDTO> GetAll(int skip = 0, int take = 20)
            => _internalService.GetAll(skip, take).Select(_mapper.EntityToShowDTO);

        public ShowAllowedFileTypesFieldDTO? GetById(int allowedFileTypeId)
            => _mapper.EntityToShowDTO(_internalService.GetById(allowedFileTypeId));

        public void Update(int id, UpdateAllowedFileTypesFieldDTO dTO)
        {
            _validator.ValidateDTO(dTO);

            var allowedFileTypesField = _internalService.GetById(id);

            _databaseBasedValidator.DatabaseBasedValidate(id, dTO);

            _mapper.UpdateEntityByDTO(allowedFileTypesField, dTO);

            _internalService.Update(allowedFileTypesField);
        }
    }
}
