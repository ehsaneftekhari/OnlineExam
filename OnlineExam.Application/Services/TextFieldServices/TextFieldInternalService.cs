using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.TextFieldServices
{
    public sealed class TextFieldInternalService 
        : BaseInternalService<TextField, ITextFieldRepository, Question, IQuestionRepository>
        , ITextFieldInternalService
    {
        public TextFieldInternalService(ITextFieldRepository repository, IQuestionInternalService parentInternalService) : base(repository, parentInternalService) { }

        protected override Expression<Func<TextField, int>> ParentIdProvider => x => x.QuestionId;
    }
}
