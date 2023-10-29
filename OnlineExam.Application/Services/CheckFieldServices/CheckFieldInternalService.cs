﻿using OnlineExam.Application.Abstractions.InternalService;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.CheckFieldServices
{
    public sealed class CheckFieldInternalService : BaseInternalService<CheckField, ICheckFieldRepository, Question, IQuestionRepository>
    {
        public CheckFieldInternalService(ICheckFieldRepository repository,
                IBaseInternalService<Question, Section> parentInternalService) : base(repository, parentInternalService) { }

        protected override Expression<Func<CheckField, int>> ParentIdProvider => x => x.QuestionId;
    }
}
