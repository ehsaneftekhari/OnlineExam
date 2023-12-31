﻿using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IValidators
{
    public interface IExamAccessValidator
    {
        bool IsUserExamCreator(string issuerUserId, Exam exam);
        bool IsUserExamCreatorOrExamUser(string userId, Exam exam);
        void ThrowIfUserIsNotExamCreator(string issuerUserId, Exam exam);
    }
}