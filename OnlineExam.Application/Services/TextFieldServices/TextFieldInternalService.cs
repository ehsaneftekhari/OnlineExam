using OnlineExam.Application.Abstractions.InternalService;
using OnlineExam.Application.Services.QuestionServices;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.TextFieldServices
{
    public sealed class TextFieldInternalService : BaseInternalService<TextField, ITextFieldRepository, Question, IQuestionRepository>
    {
        public TextFieldInternalService(ITextFieldRepository repository, QuestionInternalService parentInternalService) : base(repository, parentInternalService) { }

        protected override Expression<Func<TextField, int>> ParentIdProvider => x => x.QuestionId;
    }
}
