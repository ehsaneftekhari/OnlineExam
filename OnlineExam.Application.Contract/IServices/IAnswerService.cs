using OnlineExam.Application.Contract.DTOs.AnswerDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface IAnswerService
    {
        ShowAnswerDTO Add(AddAnswerDTO dTO);
        ShowAnswerDTO? GetById(int answerId);
        void UpdateEarnedScore(UpdateAnswerDTO dTO);
    }
}
