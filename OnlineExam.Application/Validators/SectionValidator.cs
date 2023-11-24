﻿using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Application.Abstractions.IValidators;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Validators
{
    public class SectionValidator : ISectionValidator
    {
        readonly IExamInternalService _examInternalService;
        readonly IExamValidator _examValidator;

        public SectionValidator(IExamInternalService examInternalService, IExamValidator examValidator)
        {
            _examInternalService = examInternalService;
            _examValidator = examValidator;
        }

        public void ThrowIfUserIsNotExamCreator(string issuerUserId, int examId)
        {
            _examValidator.ThrowIfUserIsNotExamCreator(issuerUserId, _examInternalService.GetById(examId));
        }

        public void ThrowIfUserIsNotExamCreator(string issuerUserId, Exam exam)
        {
            _examValidator.ThrowIfUserIsNotExamCreator(issuerUserId, exam);
        }

        public void ThrowIfUserIsNotExamCreatorOrExamUserCreator(string userId, Exam exam)
        {
            if (exam != null
                && exam.CreatorUserId != userId
                && !exam.ExamUsers.Any(x => x.UserId == userId))
                throw new ApplicationUnAuthorizedException("User has no access to Section");
        }
    }
}