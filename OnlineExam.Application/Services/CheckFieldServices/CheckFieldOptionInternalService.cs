using OnlineExam.Application.Abstractions.InternalService;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Services.QuestionServices;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.CheckFieldServices
{
    public class CheckFieldOptionInternalService : BaseInternalService<CheckFieldOption, ICheckFieldOptionRepository, Question, IQuestionRepository>
    {
        readonly CheckFieldInternalService _checkFieldInternalService;

        public CheckFieldOptionInternalService(ICheckFieldOptionRepository repository, QuestionInternalService parentInternalService) : base(repository, parentInternalService) { }

        protected override Expression<Func<CheckFieldOption, int>> ParentIdProvider => x => x.CheckFieldId;

        protected override IQueryable<CheckFieldOption> GetIQueryable() => _repository.GetIQueryable().OrderBy(q => q.Order);
    }
}
