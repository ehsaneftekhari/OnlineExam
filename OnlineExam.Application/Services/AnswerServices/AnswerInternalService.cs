using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Application.Abstractions.IInternalService;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;
using System.Linq.Expressions;

namespace OnlineExam.Application.Services.AnswerServices
{

    public sealed class AnswerInternalService
        : BaseInternalService<Answer, IAnswerRepository, ExamUser, IExamUserRepository, Question, IQuestionRepository>
        , IAnswerInternalService
    {
        public AnswerInternalService(IAnswerRepository repository,
                                     IExamUserInternalService firstParentInternalService,
                                     IQuestionInternalService secondParentInternalService) : base(repository,
                                                                                                 firstParentInternalService,
                                                                                                 secondParentInternalService)
        { }

        protected override Expression<Func<Answer, int>> FirstParentIdProvider => x => x.ExamUserId;

        protected override Expression<Func<Answer, int>> SecondParentIdProvider => x => x.QuestionId;


        internal IEnumerable<Answer> GetAllByExamUserId(int examUserId, int skip = 0, int take = 20)
            => GetAllByFirstParentId(examUserId, skip, take);

        internal IEnumerable<Answer> GetAllByExamUserIdAndQuestionId(int examUserId, int QuestionId, int skip = 0, int take = 20)
            => GetAllByParentsIds(examUserId, QuestionId, skip, take);

        IEnumerable<Answer> IAnswerInternalService.GetAllByExamUserId(int examUserId, int skip, int take)
            => GetAllByExamUserId(examUserId, skip, take);

        IEnumerable<Answer> IAnswerInternalService.GetAllByExamUserIdAndQuestionId(int examUserId, int QuestionId, int skip, int take)
            => GetAllByExamUserIdAndQuestionId(examUserId, QuestionId, skip, take);
    }
}
