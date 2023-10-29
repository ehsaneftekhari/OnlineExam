using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.FileFieldServices
{
    public sealed class FileFieldInternalService : BaseInternalService<FileField, IFileFieldRepository, Question, IQuestionRepository>
    {
        public FileFieldInternalService(IFileFieldRepository repository, IBaseInternalService<Question, Section> parentInternalService) : base(repository, parentInternalService) { }

        protected override Expression<Func<FileField, int>> ParentIdProvider => x => x.QuestionId;
    }
}
