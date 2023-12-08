using Microsoft.EntityFrameworkCore;
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
        readonly IFileFieldDTOValidator _fileFieldDTOValidator;
        readonly IQuestionComponentAccessValidator _questionComponentAccessValidator;

        public FileFieldService(IFileFieldInternalService fileFieldInternalService,
                                IFileFieldMapper fileFieldMapper,
                                IFileFieldDTOValidator fileFieldDTOValidator,
                                IQuestionComponentAccessValidator questionComponentAccessValidator)
        {
            _fileFieldInternalService = fileFieldInternalService;
            _fileFieldMapper = fileFieldMapper;
            _fileFieldDTOValidator = fileFieldDTOValidator;
            _questionComponentAccessValidator = questionComponentAccessValidator;
        }

        public ShowFileFieldDTO Add(int questionId, string issuerUserId, AddFileFieldDTO dTO)
        {
            _questionComponentAccessValidator.ThrowIfUserIsNotExamCreator(questionId, issuerUserId);
            _fileFieldDTOValidator.ValidateDTO(dTO);

            var newFileField = _fileFieldMapper.AddDTOToEntity(questionId, dTO)!;
            _fileFieldInternalService.Add(newFileField);
            return _fileFieldMapper.EntityToShowDTO(newFileField)!;
        }

        public void Delete(int fileFieldId, string issuerUserId)
        {
            var fileField = GetFileFieldWith_Question_Section_Exam_Included(fileFieldId);

            _questionComponentAccessValidator.ThrowIfUserIsNotExamCreator(issuerUserId, fileField.Question.Section.Exam);

            _fileFieldInternalService.Delete(fileField);
        }

        public IEnumerable<ShowFileFieldDTO> GetAllByQuestionId(int questionId, string issuerUserId, int skip = 0, int take = 20)
        {
            _questionComponentAccessValidator.ThrowIfUserIsNotExamCreatorOrExamUser(questionId, issuerUserId);

            return _fileFieldInternalService.GetAllByParentId(questionId, skip, take).Select(_fileFieldMapper.EntityToShowDTO);
        }

        public ShowFileFieldDTO? GetById(int fileFieldId, string issuerUserId)
        {
            var fileField = GetFileFieldWith_Question_Section_Exam_Included(fileFieldId);

            _questionComponentAccessValidator.ThrowIfUserIsNotExamCreatorOrExamUser(issuerUserId, fileField.Question.Section.Exam);

            return _fileFieldMapper.EntityToShowDTO(_fileFieldInternalService.GetById(fileFieldId));
        }
        public void Update(int fileFieldId, string issuerUserId, UpdateFileFieldDTO dTO)
        {
            var fileField = GetFileFieldWith_Question_Section_Exam_Included(fileFieldId);

            _questionComponentAccessValidator.ThrowIfUserIsNotExamCreator(issuerUserId, fileField.Question.Section.Exam);
            _fileFieldDTOValidator.ValidateDTO(dTO);

            _fileFieldMapper.UpdateEntityByDTO(fileField, dTO);
            _fileFieldInternalService.Update(fileField);
        }

        private FileField GetFileFieldWith_Question_Section_Exam_Included(int fileFieldId)
        {
            return _fileFieldInternalService.GetById(fileFieldId,
                _fileFieldInternalService.GetIQueryable()
                .Include(x => x.Question)
                .ThenInclude(x => x.Section)
                .ThenInclude(x => x.Exam));
        }
    }
}
