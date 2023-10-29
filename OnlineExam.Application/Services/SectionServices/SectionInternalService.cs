using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.SectionServices
{
    public sealed class SectionInternalService 
        : BaseInternalService<Section, ISectionRepository, Exam, IExamRepository>
        , ISectionInternalService
    {
        protected override Expression<Func<Section, int>> ParentIdProvider => x => x.ExamId;

        public SectionInternalService(ISectionRepository repository, IExamInternalService parentInternalService) : base(repository, parentInternalService) { }
    }
}
