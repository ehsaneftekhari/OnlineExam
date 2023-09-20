using OnlineExam.Application.Abstractions.InternalService;
using OnlineExam.Application.Services.ExamServices;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services
{
    public sealed class ExamUserInternalService : BaseInternalService<ExamUser, IExamUserRepository, Exam, IExamRepository>
    {
        public ExamUserInternalService(IExamUserRepository repository, ExamInternalService parentInternalService) : base(repository, parentInternalService) { }

        protected override Expression<Func<ExamUser, int>> ParentIdProvider => x => x.ExamId;

        internal override void Update(ExamUser record) => throw new NotImplementedException();
    }
}
