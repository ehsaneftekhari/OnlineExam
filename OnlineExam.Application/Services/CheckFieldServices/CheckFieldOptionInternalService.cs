using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.CheckFieldServices
{
    public sealed class CheckFieldOptionInternalService 
        : BaseInternalService<CheckFieldOption, ICheckFieldOptionRepository, CheckField, ICheckFieldRepository>
        , ICheckFieldOptionInternalService
    {
        public CheckFieldOptionInternalService(ICheckFieldOptionRepository repository,
            ICheckFieldInternalService parentInternalService) : base(repository, parentInternalService) { }

        protected override Expression<Func<CheckFieldOption, int>> ParentIdProvider => x => x.CheckFieldId;

        internal override IQueryable<CheckFieldOption> GetIQueryable() => _repository.GetIQueryable().OrderBy(q => q.Order);
    }
}
