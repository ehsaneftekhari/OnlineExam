using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Attributes;
using OnlineExam.Application.Contract.DTOs.ExamDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.IMappers;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System;
using System.Linq;

namespace OnlineExam.Application.Services
{
    public class ExamService : IExamService
    {
        readonly OnlineExamContext _context;
        readonly IExamRepository _examRepository;
        readonly ITagRepository _tagRepository;
        readonly IExamMapper _examMapper;
        readonly ITagService _tagService;

        public ExamService(IExamRepository examRepository, IExamMapper examMapper, ITagService tagService, ITagRepository tagRepository, OnlineExamContext context)
        {
            this._examRepository = examRepository;
            this._examMapper = examMapper;
            _tagService = tagService;
            _tagRepository = tagRepository;
            _context = context;
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
            //Test2GetByFilter(dTO);
            //Test3GetByFilter(dTO);
            //Test4GetByFilter(dTO);
            //Test5GetByFilter(dTO);
            //Test6GetByFilter(dTO);
            //Test7GetByFilter(dTO);
            //return Test2GetByFilter(dTO);
            return Test7GetByFilter(dTO);
        }

        private PagingModel<List<ShowExamDTO>> Test7GetByFilter(ExamFilterDTO dTO)
        {
            //throws exception
            var quarry = _context.Tag 
                .Where(tag => dTO.Tags == default || dTO.Tags.Count == 0 || dTO.Tags.Contains(tag.Name))
                .Join(_context.Set<ExamTag>(nameof(ExamTag))
                    , tag => tag.Id
                    , examTag => examTag.TagId
                    , (tag, examTag) => examTag.ExamId
                )
                .GroupJoin(_context.Exam
                    , examId => examId
                    , exam => exam.Id
                    , (examId, exam) => exam
                )
                .SelectMany(exams => exams)
                .Where(exam => dTO.Title == default || exam.Title.Contains(dTO.Title))
                .Where(exam => !dTO.StartFrom.HasValue || dTO.StartFrom.Value <= exam.Start)
                .Where(exam => !dTO.StartTo.HasValue || dTO.StartTo.Value >= exam.Start)
                .Where(exam => !dTO.EndFrom.HasValue || dTO.EndFrom.Value <= exam.End)
                .Where(exam => !dTO.EndTo.HasValue || dTO.EndTo.Value >= exam.End)
                .Where(exam => !dTO.Published.HasValue || dTO.Published.Value == exam.Published);

            var t = quarry.ToList();

            var exams = quarry
                .Skip(dTO.Skip)
                .Take(dTO.Take)
                .ToList()
                .Select(exam => _examMapper.EntityToShowDTO(exam))
                .ToList();

            return new(exams, quarry.Count());
        }

        private PagingModel<List<ShowExamDTO>> Test6GetByFilter(ExamFilterDTO dTO)
        {
            //throws exception
            var quarry = _examRepository.GetIQueryable()
                .Join(_context.Set<ExamTag>(nameof(ExamTag))
                    , exam => exam.Id
                    , examTag => examTag.ExamId
                    , (exam, tagTag) => new { exam, tagTag.TagId }
                )
                .Join(_context.Set<Tag>()
                    , obj => obj.TagId
                    , tag => tag.Id
                    , (obj, tag) => new { obj.exam, tag }
                )
                .Where(join => dTO.Tags == default || dTO.Tags.Count == 0 || dTO.Tags.Contains(join.tag.Name))
                .Select(join => join.exam)
                .Where(exam => dTO.Title == default || exam.Title.Contains(dTO.Title))
                .Where(exam => !dTO.StartFrom.HasValue || dTO.StartFrom.Value <= exam.Start)
                .Where(exam => !dTO.StartTo.HasValue || dTO.StartTo.Value >= exam.Start)
                .Where(exam => !dTO.EndFrom.HasValue || dTO.EndFrom.Value <= exam.End)
                .Where(exam => !dTO.EndTo.HasValue || dTO.EndTo.Value >= exam.End)
                .Where(exam => !dTO.Published.HasValue || dTO.Published.Value == exam.Published)
                .ToList()
                .GroupBy(exam => exam)
                .Select(gr => gr.Key)
                .ToList();

            var exams = quarry
                .Skip(dTO.Skip)
                .Take(dTO.Take)
                .ToList()
                .Select(exam => _examMapper.EntityToShowDTO(exam))
                .ToList();

            return new(exams, quarry.Count());
        }

        public class ExamTagResult : IEquatable<ExamTagResult>
        {
            public ExamTagResult(Exam exam, Tag tag)
            {
                Exam = exam;
                Tag = tag;
            }

            public Exam Exam { get; set; }
            public Tag Tag { get; set; }

            public bool Equals(ExamTagResult other)
            {
                if (other == null) return false;
                return Exam.Id == other.Exam.Id && Tag.Id == other.Tag.Id;
            }

            public override int GetHashCode()
            {
                return Exam.Id.GetHashCode() ^ Tag.Id.GetHashCode();
            }
        }

        private PagingModel<List<ShowExamDTO>> Test5GetByFilter(ExamFilterDTO dTO)
        {
            //throws exception
            var quarry = _examRepository.GetIQueryable()
            .Join(_context.Set<ExamTag>(nameof(ExamTag))
                , exam => exam.Id
                , examTag => examTag.ExamId
                , (exam, tagTag) => new { exam, tagTag.TagId }
            )
            .Join(_context.Set<Tag>()
                , obj => obj.TagId
                , tag => tag.Id
                , (obj, tag) => new ExamTagResult(obj.exam, tag )
            )
            .GroupBy(join => join.Exam)
            .ToList();
            //.Select(gr => gr.Key)

            //.Where(exam => dTO.Tags == default || dTO.Tags.Count == 0 || exam.Tags.Any(t => dTO.Tags.Contains(t.Name)))
            //.Where(exam => dTO.Title == default || exam.Title.Contains(dTO.Title))
            //.Where(exam => !dTO.StartFrom.HasValue || dTO.StartFrom.Value <= exam.Start)
            //.Where(exam => !dTO.StartTo.HasValue || dTO.StartTo.Value >= exam.Start)
            //.Where(exam => !dTO.EndFrom.HasValue || dTO.EndFrom.Value <= exam.End)
            //.Where(exam => !dTO.EndTo.HasValue || dTO.EndTo.Value >= exam.End)
            //.Where(exam => !dTO.Published.HasValue || dTO.Published.Value == exam.Published);


            //var exams = quarry
            //    .Skip(dTO.Skip)
            //    .Take(dTO.Take)
            //    .ToList()
            //    .Select(exam => _examMapper.EntityToShowDTO(exam))
            //    .ToList();

            //return new(exams, quarry.Count());
            return new(null, 0);
        }

        private PagingModel<List<ShowExamDTO>> Test4GetByFilter(ExamFilterDTO dTO)
        {
            //throws exception
            var quarry = _examRepository.GetIQueryable()
                .SelectMany(exam => exam.Tags, (exam, tag) => new { exam, tag })
                .GroupBy(obj => obj.exam)
                .Select(gr => gr.Key);
            //.Where(exam => dTO.Title == default || exam.Title.Contains(dTO.Title))
            //.Where(exam => !dTO.StartFrom.HasValue || dTO.StartFrom.Value <= exam.Start)
            //.Where(exam => !dTO.StartTo.HasValue || dTO.StartTo.Value >= exam.Start)
            //.Where(exam => !dTO.EndFrom.HasValue || dTO.EndFrom.Value <= exam.End)
            //.Where(exam => !dTO.EndTo.HasValue || dTO.EndTo.Value >= exam.End)
            //.Where(exam => !dTO.Published.HasValue || dTO.Published.Value == exam.Published);

            //var t = quarry.ToList();

            //var exams = quarry
            //    .Skip(dTO.Skip)
            //    .Take(dTO.Take)
            //    .ToList()
            //    .Select(exam => _examMapper.EntityToShowDTO(exam))
            //    .ToList();

            //return new(exams, quarry.Count());
            return new(null, 0);
        }

        private PagingModel<List<ShowExamDTO>> Test3GetByFilter(ExamFilterDTO dTO)
        {
            var quarry = _context.Exam
                .SelectMany(
                    exam => exam.Tags
                        .Where(tag => dTO.Tags == default || dTO.Tags.Count == 0 || dTO.Tags.Contains(tag.Name))
                    , (exam, tag) => new { exam, tag }
                )
                .Where(obj => dTO.Title == default || obj.exam.Title.Contains(dTO.Title))
                .Where(obj => !dTO.StartFrom.HasValue || dTO.StartFrom.Value <= obj.exam.Start)
                .Where(obj => !dTO.StartTo.HasValue || dTO.StartTo.Value >= obj.exam.Start)
                .Where(obj => !dTO.EndFrom.HasValue || dTO.EndFrom.Value <= obj.exam.End)
                .Where(obj => !dTO.EndTo.HasValue || dTO.EndTo.Value >= obj.exam.End)
                .Where(obj => !dTO.Published.HasValue || dTO.Published.Value == obj.exam.Published)
                .Select(obj => obj.exam)
                .ToList()
                .GroupBy(exam => exam);

            //var exams = quarry
            //    .Skip(dTO.Skip)
            //    .Take(dTO.Take)
            //    .ToList()
            //    .Select(exam => _examMapper.EntityToShowDTO(exam))
            //    .ToList();

            //return new(exams, quarry.Count());
            return new(null, 0);
        }

        private PagingModel<List<ShowExamDTO>> Test2GetByFilter(ExamFilterDTO dTO)
        {
            var quarry = _examRepository.GetIQueryable()
            .Include(exam => exam.Tags)
            .Where(exam => dTO.Tags == default || dTO.Tags.Count == 0 || exam.Tags.Any(t => dTO.Tags.Contains(t.Name)))
            .Where(exam => dTO.Title == default || exam.Title.Contains(dTO.Title))
            .Where(exam => !dTO.StartFrom.HasValue || dTO.StartFrom.Value <= exam.Start)
            .Where(exam => !dTO.StartTo.HasValue || dTO.StartTo.Value >= exam.Start)
            .Where(exam => !dTO.EndFrom.HasValue || dTO.EndFrom.Value <= exam.End)
            .Where(exam => !dTO.EndTo.HasValue || dTO.EndTo.Value >= exam.End)
            .Where(exam => !dTO.Published.HasValue || dTO.Published.Value == exam.Published);

            var t = quarry.ToList();

            var exams = quarry
                .Skip(dTO.Skip)
                .Take(dTO.Take)
                .ToList()
                .Select(exam => _examMapper.EntityToShowDTO(exam))
                .ToList();

            return new(exams, quarry.Count());
        }

        private PagingModel<List<ShowExamDTO>> Test1GetByFilter(ExamFilterDTO dTO)
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

            var quarry8 = quarry7.Where(exam => dTO.Tags == default || dTO.Tags.Count == 0 || exam.Tags.All(t => dTO.Tags.Contains(t.Name)));
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
