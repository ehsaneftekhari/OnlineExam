using OnlineExam.Application.Abstractions.InternalService;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.SectionServices
{
    public sealed class SectionInternalService : BaseInternalService<Section, ISectionRepository, Exam, IExamRepository>
    {
        protected override Expression<Func<Section, int>> ParentIdProvider => x => x.ExamId;

        public SectionInternalService(ISectionRepository repository, IBaseInternalService<Exam> parentInternalService) : base(repository, parentInternalService) { }
    }
}
