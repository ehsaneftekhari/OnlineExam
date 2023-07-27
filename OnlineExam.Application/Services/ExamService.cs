using OnlineExam.Application.Contract.DTOs.ExamDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.IMappers;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services
{
    public class ExamService : IExamService
    {
        readonly IExamRepository _examRepository;
        readonly IExamMapper _examMapper;
        readonly ITagService _tagService;

        public ExamService(IExamRepository examRepository, IExamMapper examMapper, ITagService tagService)
        {
            this._examRepository = examRepository;
            this._examMapper = examMapper;
            _tagService = tagService;
        }

        public bool Add(AddExamDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            var tagIds = _tagService.GetOrAdd(dTO.Tags.ToList()).Select(x => x.Id);
            var newExam = _examMapper.AddDTOToEntity(dTO);
            newExam!.CreatorUserId = "1";
            var result = _examRepository.Add(newExam) == 1;

            _examMapper.UpdateExamTags(newExam, tagIds);

            result = result & _examRepository.Update(newExam) == tagIds.Count() + 1;

            return result;
        }

        public bool Delete(int id)
        {
            var exam = _examRepository.GetFullyLoaded(id);
            if(exam == null || exam.Sections.Count != 0)
                return false;
            return _examRepository.DeleteByEntity(exam) == 1;
        }

        public ShowExamDTO? GetById(int id)
        {
            return _examMapper.EntityToShowDTO(_examRepository.GetFullyLoaded(id));
        }

        public bool Update(UpdateExamDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            var exam = _examRepository.GetById(dTO.Id);

            var result = false;

            if(exam != null)
            {
                _examMapper.UpdateEntityByDTO(exam, dTO);
                result = _examRepository.Update(exam) == 1;
            }

            return result;
        }
    }
}
