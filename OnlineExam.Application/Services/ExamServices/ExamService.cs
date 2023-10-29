using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Contract.DTOs.ExamDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.ExamServices
{
    public sealed class ExamService : IExamService
    {
        readonly IBaseInternalService<Exam> _internalService;
        readonly IExamMapper _examMapper;

        public ExamService(IExamMapper examMapper, IBaseInternalService<Exam> internalService)
        {
            _examMapper = examMapper;
            _internalService = internalService;
        }

        public ShowExamDTO Add(AddExamDTO dTO)
        {
            var newExam = _examMapper.AddDTOToEntity(dTO);
            newExam!.CreatorUserId = "1";
            _internalService.Add(newExam);
            return _examMapper.EntityToShowDTO(newExam)!;
        }

        public void Delete(int id) => _internalService.Delete(id);

        public ShowExamDTO? GetById(int id) => _examMapper.EntityToShowDTO(_internalService.GetById(id));

        public IEnumerable<ShowExamDTO> GetAll(int skip, int take) => _internalService.GetAll(skip, take).Select(_examMapper.EntityToShowDTO)!;

        public void Update(int id, UpdateExamDTO dTO)
        {
            var exam = _internalService.GetById(id);

            _examMapper.UpdateEntityByDTO(exam, dTO);

            _internalService.Update(exam);
        }
    }
}
