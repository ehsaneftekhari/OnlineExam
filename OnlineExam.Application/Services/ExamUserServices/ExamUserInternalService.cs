using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.ExamUserServices
{
    public sealed class ExamUserInternalService 
        : BaseInternalService<ExamUser, IExamUserRepository, Exam, IExamRepository>
        , IExamUserInternalService
    {
        public ExamUserInternalService(IExamUserRepository repository,
            IExamInternalService parentInternalService) : base(repository, parentInternalService) { }

        protected override Expression<Func<ExamUser, int>> ParentIdProvider => x => x.ExamId;
    }
}
