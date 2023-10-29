using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.TextFieldServices
{
    public sealed class TextFieldInternalService : BaseInternalService<TextField, ITextFieldRepository, Question, IQuestionRepository>
    {
        public TextFieldInternalService(ITextFieldRepository repository, IBaseInternalService<Question, Section> parentInternalService) : base(repository, parentInternalService) { }

        protected override Expression<Func<TextField, int>> ParentIdProvider => x => x.QuestionId;
    }
}
