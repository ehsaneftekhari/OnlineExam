using OnlineExam.Application.Contract.DTOs.AnswerDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface IAnswerService
    {
        ShowAnswerDTO Add(AddAnswerDTO dTO, string issuerUserId);
        ShowAnswerDTO? GetById(int answerId, string issuerUserId);
        IEnumerable<ShowAnswerDTO> GetAll(int examUserId, int questionId, string issuerUserId, int skip, int take);
        IEnumerable<ShowAnswerDTO> GetAllByExamUserId(int examUserId, string issuerUserId, int skip, int take);
        void UpdateEarnedScore(UpdateAnswerDTO dTO, string issuerUserId);
    }
}
