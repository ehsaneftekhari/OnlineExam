using OnlineExam.Application.Contract.DTOs.ExamDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface IExamService
    {
        ShowExamDTO Add(AddExamDTO dTO);

        ShowExamDTO? GetById(int id);

        void Update(UpdateExamDTO dTO);

        void Delete(int id);
    }
}
