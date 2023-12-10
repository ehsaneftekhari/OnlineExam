using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.FileFieldServices
{
    public sealed class FileFieldInternalService 
        : BaseInternalService<FileField, IFileFieldRepository, Question, IQuestionRepository>
        , IFileFieldInternalService
    {
        public FileFieldInternalService(IFileFieldRepository repository
            , IQuestionInternalService parentInternalService) : base(repository, parentInternalService) { }

        protected override Expression<Func<FileField, int>> ParentIdProvider => x => x.QuestionId;
    }
}
