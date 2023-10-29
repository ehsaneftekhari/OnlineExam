using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.DTOs.FileFieldDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.FileFieldServices
{
    public sealed class FileFieldService : IFileFieldService
    {
        readonly IFileFieldInternalService _fileFieldInternalService;
        readonly IFileFieldMapper _fileFieldMapper;
        readonly IFileFieldValidator _fileFieldValidator;

        public FileFieldService(IFileFieldInternalService fileFieldInternalService,
                                IFileFieldMapper fileFieldMapper,
                                IFileFieldValidator fileFieldValidator)
        {
            _fileFieldInternalService = fileFieldInternalService;
            _fileFieldMapper = fileFieldMapper;
            _fileFieldValidator = fileFieldValidator;
        }

        public ShowFileFieldDTO Add(int questionId, AddFileFieldDTO dTO)
        {
            _fileFieldValidator.ValidateDTO(dTO);
            var newFileField = _fileFieldMapper.AddDTOToEntity(questionId, dTO)!;
            _fileFieldInternalService.Add(newFileField);
            return _fileFieldMapper.EntityToShowDTO(newFileField)!;
        }

        public void Delete(int fileFieldId)
            => _fileFieldInternalService.Delete(fileFieldId);

        public IEnumerable<ShowFileFieldDTO> GetAllByQuestionId(int questionId, int skip = 0, int take = 20)
            => _fileFieldInternalService.GetAllByParentId(questionId, skip, take).Select(_fileFieldMapper.EntityToShowDTO);

        public ShowFileFieldDTO? GetById(int fileFieldId)
            => _fileFieldMapper.EntityToShowDTO(_fileFieldInternalService.GetById(fileFieldId));

        public void Update(int fileFieldId, UpdateFileFieldDTO dTO)
        {
            _fileFieldValidator.ValidateDTO(dTO);
            var fileField = _fileFieldInternalService.GetById(fileFieldId);
            _fileFieldMapper.UpdateEntityByDTO(fileField, dTO);
            _fileFieldInternalService.Update(fileField);
        }
    }
}
