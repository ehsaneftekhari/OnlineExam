using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.QuestionServices
{
    public sealed class QuestionInternalService 
        : BaseInternalService<Question, IQuestionRepository, Section, ISectionRepository>
        , IQuestionInternalService
    {
        protected override Expression<Func<Question, int>> ParentIdProvider => x => x.SectionId;

        public QuestionInternalService(IQuestionRepository repository
            , ISectionInternalService parentInternalService) : base(repository, parentInternalService) { }
    }
}
