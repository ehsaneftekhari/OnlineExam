using OnlineExam.Application.Contract.DTOs.ExamUserDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface IExamUserService
    {
        ShowExamUserDTO Add(AddExamUserDTO dTO);
        ShowExamUserDTO? GetById(int id);
        IEnumerable<ShowExamUserDTO> GetAllByExamId(int examId, int skip = 0, int take = 20);
        void Delete(int id);
    }
}