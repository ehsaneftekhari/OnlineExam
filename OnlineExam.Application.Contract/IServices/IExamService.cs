using OnlineExam.Application.Contract.DTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface IExamService
    {
        bool Add(AddExamDTO dTO);

        ShowExamDTO GetById(int id);

        bool Update(UpdateExamDTO dTO);
    }
}
