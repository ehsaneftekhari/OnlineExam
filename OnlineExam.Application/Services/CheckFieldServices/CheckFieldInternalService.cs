using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.CheckFieldServices
{
    public sealed class CheckFieldInternalService
        : BaseInternalService<CheckField, ICheckFieldRepository, Question, IQuestionRepository>
        , ICheckFieldInternalService
    {
        public CheckFieldInternalService(ICheckFieldRepository repository,
                IQuestionInternalService parentInternalService) : base(repository, parentInternalService) { }

        protected override Expression<Func<CheckField, int>> ParentIdProvider => x => x.QuestionId;
    }
}
