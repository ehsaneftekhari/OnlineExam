using OnlineExam.Application.Contract.DTOs.ExamUserDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface IExamUserService
    {
        ShowExamUserDTO Add(AddExamUserDTO dTO);
        ShowExamUserDTO? GetById(int id, string issuerUserId);
        IEnumerable<ShowExamUserDTO> GetAllByExamId(int examId, string issuerUserId, int skip = 0, int take = 20);
        void Delete(int id, string issuerUserId);
        void Finish(int id, string issuerUserId);
    }
}