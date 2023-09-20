using OnlineExam.Application.Abstractions.InternalService;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.Services.QuestionServices;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.FileFieldServices
{
    public class FileFieldInternalService : BaseInternalService<FileField, IFileFieldRepository, Question, IQuestionRepository>
    {
        public FileFieldInternalService(IFileFieldService repository, QuestionInternalService parentInternalService) : base(repository, parentInternalService){}

        protected override Expression<Func<FileField, int>> ParentIdProvider => x => x.QuestionId;
    }
}
