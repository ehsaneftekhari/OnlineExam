﻿using OnlineExam.Application.Contract.DTOs.AnswerDTOs;

namespace OnlineExam.Application.Contract.IServices
{
    public interface IAnswerService
    {
        ShowAnswerDTO Add(AddAnswerDTO dTO);
        ShowAnswerDTO? GetById(int answerId);
        IEnumerable<ShowAnswerDTO> GetAll(int examUserId, int questionId, int skip, int take);
        IEnumerable<ShowAnswerDTO> GetAllByExamUserId(int examUserId, int skip, int take);
        void UpdateEarnedScore(UpdateAnswerDTO dTO);
    }
}
