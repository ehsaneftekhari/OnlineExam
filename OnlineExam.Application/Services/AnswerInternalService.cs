using OnlineExam.Application.Abstractions.InternalService;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Application.Services.ExamUserServices;
using OnlineExam.Application.Services.QuestionServices;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services
{
    public sealed class AnswerInternalService : BaseInternalService<Answer, IAnswerRepository, ExamUser, IExamUserRepository, Question, IQuestionRepository>
    {
        public AnswerInternalService(IAnswerRepository repository,
                                     ExamUserInternalService firstParentInternalService,
                                     QuestionInternalService secondParentInternalService) : base(repository,
                                                                                                 firstParentInternalService,
                                                                                                 secondParentInternalService) { }

        protected override Expression<Func<Answer, int>> FirstParentIdProvider => x => x.ExamUserId;

        protected override Expression<Func<Answer, int>> SecondParentIdProvider => x => x.QuestionId;


        internal IEnumerable<Answer> GetAllByExamUserId(int examUserId, int skip = 0, int take = 20)
            => GetAllByFirstParentId(examUserId, skip, take);

        internal IEnumerable<Answer> GetAllByExamUserIdAndQuestionId(int examUserId, int QuestonId, int skip = 0, int take = 20)
            => GetAllByParentsIds(examUserId, QuestonId, skip, take);
    }
}
