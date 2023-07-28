using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Attributes;
using OnlineExam.Application.Contract.DTOs.ExamDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.IMappers;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq;

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

        [TransactionUnitOfWork]
        public bool Add(AddExamDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            var newExam = _examMapper.AddDTOToEntity(dTO);
            newExam!.Tags = _tagService.SyncTagsByNames(newExam!.Tags).ToList();
            newExam!.CreatorUserId = "1";
            return _examRepository.Add(newExam) > 0;
        }

        public bool Delete(int id)
        {
            var exam = _examRepository.GetFullyLoaded(id);
            if (exam == null || exam.Sections.Count != 0)
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

            if (exam != null)
            {
                _examMapper.UpdateEntityByDTO(exam, dTO);
                result = _examRepository.Update(exam) == 1;
            }

            return result;
        }

        public PagingModel<List<ShowExamDTO>> GetByFilter(ExamFilterDTO dTO)
        {
            var quarry1 = _examRepository.GetIQueryable()
                .Include(exam => exam.Tags);

            var t1 = quarry1.ToList();


            var quarry2 = quarry1.Where(exam => dTO.Title == default || exam.Title.Contains(dTO.Title));
            var t2 = quarry2.ToList();

            var quarry3 = quarry2.Where(exam => !dTO.StartFrom.HasValue || dTO.StartFrom.Value <= exam.Start);
            var t3 = quarry3.ToList();

            var quarry4 = quarry3.Where(exam => !dTO.StartTo.HasValue || dTO.StartTo.Value >= exam.Start);
            var t4 = quarry4.ToList();

            var quarry5 = quarry4.Where(exam => !dTO.EndFrom.HasValue || dTO.EndFrom.Value <= exam.End);
            var t5 = quarry5.ToList();

            var quarry6 = quarry5.Where(exam => !dTO.EndTo.HasValue || dTO.EndTo.Value >= exam.End);
            var t6 = quarry6.ToList();

            var quarry7 = quarry6.Where(exam => !dTO.Published.HasValue || dTO.Published.Value == exam.Published);
            var t7 = quarry7.ToList();

            var quarry8 = quarry7.Where(exam => dTO.Tags == default || dTO.Tags.Count == 0 || exam.Tags.Any(t => dTO.Tags.Contains(t.Name)));
            var t8 = quarry8.ToList();




            var exams = quarry8
                .Skip(dTO.Skip)
                .Take(dTO.Take)
                .ToList()
                .Select(exam => _examMapper.EntityToShowDTO(exam))
                .ToList();

            return new(exams, quarry8.Count());
        }
    }
}
