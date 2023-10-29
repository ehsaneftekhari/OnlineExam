using OnlineExam.Application.Abstractions.InternalService;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.ExamUserServices
{
    public sealed class ExamUserInternalService : BaseInternalService<ExamUser, IExamUserRepository, Exam, IExamRepository>
    {
        public ExamUserInternalService(IExamUserRepository repository,
            IBaseInternalService<Exam> parentInternalService) : base(repository, parentInternalService) { }

        protected override Expression<Func<ExamUser, int>> ParentIdProvider => x => x.ExamId;

        internal override void Update(ExamUser record) => throw new NotImplementedException();
    }
}
