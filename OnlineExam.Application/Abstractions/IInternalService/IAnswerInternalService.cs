using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IInternalService
{
    public interface IAnswerInternalService : IBaseInternalService<Answer, int, ExamUser, int, Question, int>
    {
        internal IEnumerable<Answer> GetAllByExamUserId(int examUserId, int skip = 0, int take = 20);
        internal IEnumerable<Answer> GetAllByExamUserId(int examUserId, Func<IQueryable<Answer>, IQueryable<Answer>> externQueryProvider, int skip, int take);
        internal IEnumerable<Answer> GetAllByExamUserIdAndQuestionId(int examUserId, int QuestionId, int skip = 0, int take = 20);
    }
}
