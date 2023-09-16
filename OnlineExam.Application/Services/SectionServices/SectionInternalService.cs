using OnlineExam.Application.Abstractions.InternalService;
using OnlineExam.Application.Services.ExamServices;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.SectionServices
{
    public class SectionInternalService : BaseInternalService<Section, ISectionRepository, Exam, IExamRepository>
    {
        protected override Expression<Func<Section, int>> ParentIdProvider => x => x.ExamId;

        public SectionInternalService(ISectionRepository repository, ExamInternalService parentInternalService) : base(repository, parentInternalService) { }
    }
}
