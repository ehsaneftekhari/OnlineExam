using OnlineExam.Application.Abstractions.IMappers;
using OnlineExam.Application.Contract.DTOs.FileFieldDTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Infrastructure.Contract.IRepositories;

namespace OnlineExam.Application.Services
{
    public class FileFieldService : IFileFieldService
    {
        readonly IFileFieldOptionRepository _optionRepository;
        readonly IQuestionRepository _questionRepository;
        readonly IFileFieldMapper _fileFieldMapper;

        public FileFieldService(IFileFieldOptionRepository optionRepository, IQuestionRepository questionRepository, IFileFieldMapper fileFieldMapper)
        {
            _optionRepository = optionRepository;
            _questionRepository = questionRepository;
            _fileFieldMapper = fileFieldMapper;
        }

        public ShowFileFieldDTO Add(int questionId, AddFileFieldDTO dTO)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ShowFileFieldDTO> GetAllByQuestionId(int questionId, int skip = 0, int take = 20)
        {
            throw new NotImplementedException();
        }

        public ShowFileFieldDTO? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, UpdateFileFieldDTO dTO)
        {
            throw new NotImplementedException();
        }
    }
}
