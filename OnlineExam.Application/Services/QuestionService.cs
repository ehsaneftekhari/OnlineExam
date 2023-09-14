using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Contract.DTOs.QuestionDTOs;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Infrastructure.Contract.IRepositories;

namespace OnlineExam.Application.Services
{
    public class QuestionService : IQuestionService
    {
        readonly IQuestionRepository _questionRepository;
        readonly ISectionRepository _sectionRepository;
        readonly IQuestionMapper _questionMapper;

        public QuestionService(IQuestionRepository questionRepository, IQuestionMapper questionMapper, ISectionRepository sectionRepository)
        {
            _questionRepository = questionRepository;
            _questionMapper = questionMapper;
            _sectionRepository = sectionRepository;
        }

        public ShowQuestionDTO Add(int sectionId, AddQuestionDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            if (sectionId < 1)
                throw new ApplicationValidationException("SectionId can not be less than 1");

            if (string.IsNullOrEmpty(dTO.Text))
                throw new ApplicationValidationException("Text can not be null Or empty");

            try
            {
                var newQuestion = _questionMapper.AddDTOToEntity(sectionId, dTO);
                if (_questionRepository.Add(newQuestion) > 0 && newQuestion.Id > 0)
                    return _questionMapper.EntityToShowDTO(newQuestion)!;

                throw new Exception();
            }
            catch
            {
                if (_sectionRepository.GetById(sectionId) == null)
                    throw new OEApplicationException($"SectionId with id:{sectionId} is not exists");

                throw;
            }
        }

        public IEnumerable<ShowQuestionDTO> GetAllBySectionId(int sectionId, int skip, int take)
        {
            if (sectionId < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var questions = 
                _questionRepository.GetIQueryable()
                .Where(q => q.SectionId == sectionId)
                .Skip(skip)
                .Take(take)
                .ToList()
                .Select(_questionMapper.EntityToShowDTO);

            if (!questions.Any())
            {
                if (_sectionRepository.GetById(sectionId) == null)
                    throw new ApplicationSourceNotFoundException($"SectionId with id:{sectionId} is not exists");

                throw new ApplicationSourceNotFoundException($"there is no question within Section by id:{sectionId}");
            }

            return questions!;
        }

        public ShowQuestionDTO? GetById(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var question = _questionRepository.GetById(id);

            if (question == null)
                throw new ApplicationSourceNotFoundException($"Question with id:{id} is not exists");

            return _questionMapper.EntityToShowDTO(question);
        }

        public void Update(int id, UpdateQuestionDTO dTO)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            if (dTO == null)
                throw new ArgumentNullException();

            var question = _questionRepository.GetById(id);

            if (question == null)
                throw new ApplicationSourceNotFoundException($"Question with id:{id} is not exists");

            _questionMapper.UpdateEntityByDTO(question, dTO);

            if (_questionRepository.Update(question) <= 0)
                throw new Exception();
        }

        public void Delete(int id)
        {
            if (id < 1)
                throw new ApplicationValidationException("id can not be less than 1");

            var question = _questionRepository.GetById(id);

            if (question == null)
                throw new ApplicationSourceNotFoundException($"Question with id:{id} is not exists");

            if (_questionRepository.Delete(question) < 0)
                throw new Exception();
        }
    }
}
