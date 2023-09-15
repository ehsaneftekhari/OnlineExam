using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Services.SectionServices;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Services.QuestionServices
{
    public class QuestionInternalService
    {
        readonly IQuestionRepository _questionRepository;
        readonly SectionInternalService _sectionInternalService;

        public QuestionInternalService(IQuestionRepository questionRepository, SectionInternalService sectionInternalService)
        {
            _questionRepository = questionRepository;
            _sectionInternalService = sectionInternalService;
        }

        internal Question Add(Question newQuestion)
        {
            ThrowIfQuestionIsNotValid(newQuestion);

            _sectionInternalService.ThrowIfSectionIdIsNotValid(newQuestion.SectionId);

            try
            {
                if (_questionRepository.Add(newQuestion) > 0 && newQuestion.Id > 0)
                    return newQuestion;

                throw new Exception();
            }
            catch
            {
                _sectionInternalService.ThrowExceptionIfSectionIsNotExists(newQuestion.SectionId);

                throw;
            }
        }

        internal IEnumerable<Question> GetAllBySectionId(int sectionId, int skip, int take)
        {
            _sectionInternalService.ThrowIfSectionIdIsNotValid(sectionId);

            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var questions =
                _questionRepository.GetIQueryable()
                .Where(q => q.SectionId == sectionId)
                .Skip(skip)
                .Take(take)
                .ToList();

            if (!questions.Any())
            {
                _sectionInternalService.ThrowExceptionIfSectionIsNotExists(sectionId);

                throw new ApplicationSourceNotFoundException($"there is no question within Section by id:{sectionId}");
            }

            return questions!;
        }

        internal Question GetById(int questionId)
        {
            ThrowIfQuestionIdIsNotValid(questionId);

            var question = _questionRepository.GetById(questionId);

            if (question == null)
                throw new ApplicationSourceNotFoundException($"Question with id:{questionId} is not exists");

            return question;
        }

        internal void ThrowExceptionIfQuestionIsNotExists(int questionId) => GetById(questionId);

        public void Update(Question question)
        {
            ThrowIfQuestionIsNotValid(question);

            if (_questionRepository.Update(question) <= 0)
                throw new OEApplicationException("Question did not updated");
        }

        public void Delete(int questionId)
        {
            ThrowIfQuestionIdIsNotValid(questionId);

            var question = GetById(questionId);

            if (_questionRepository.Delete(question) < 0)
                throw new OEApplicationException("Question did not deleted");
        }

        internal void ThrowIfQuestionIdIsNotValid(int questionId)
        {
            if (questionId < 1)
                throw new ApplicationValidationException($"{nameof(questionId)} can not be less than 1");
        }

        internal void ThrowIfQuestionIsNotValid(Question question)
        {
            if (question == null)
                throw new ArgumentNullException();
        }
    }
}
