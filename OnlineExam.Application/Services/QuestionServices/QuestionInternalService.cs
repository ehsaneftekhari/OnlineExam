using OnlineExam.Application.Abstractions.InternalService;
using OnlineExam.Application.Services.SectionServices;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.QuestionServices
{
    public sealed class QuestionInternalService : BaseInternalService<Question, IQuestionRepository, Section, ISectionRepository>
    {
        protected override Expression<Func<Question, int>> ParentIdProvider => x => x.SectionId;

        public QuestionInternalService(IQuestionRepository repository, SectionInternalService parentInternalService) : base(repository, parentInternalService) { }
    }
}
