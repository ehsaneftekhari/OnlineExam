using OnlineExam.Application.Contract.DTOs;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.IMappers;
using OnlineExam.Infrastructure.Contract.IRepositories;

namespace OnlineExam.Application.Services
{
    public class ExamService : IExamService
    {
        readonly IExamRepository _examRepository;
        readonly IExamMapper _examMapper;

        public ExamService(IExamRepository examRepository, IExamMapper examMapper)
        {
            this._examRepository = examRepository;
            this._examMapper = examMapper;
        }

        public bool Add(AddExamDTO dTO)
        {
            if (dTO == null)
                throw new ArgumentNullException();

            var newExam = _examMapper.AddDTOToEntity(dTO);
            newExam!.CreatorUserId = "1";
            return _examRepository.Add(newExam) == 1;
        }

        public bool Delete(int id)
        {
            return _examRepository.Delete(id) == 1;
        }

        public ShowExamDTO? GetById(int id)
        {
            return _examMapper.EntityToShowDTO(_examRepository.GetById(id));
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
