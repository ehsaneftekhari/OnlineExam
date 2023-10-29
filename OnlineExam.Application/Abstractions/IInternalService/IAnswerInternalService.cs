using OnlineExam.Application.Abstractions.BaseInternalServices;
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.IInternalService
{
    public interface IAnswerInternalService : IBaseInternalService<Answer, ExamUser, Question>
    {
        internal IEnumerable<Answer> GetAllByExamUserId(int examUserId, int skip = 0, int take = 20);
        internal IEnumerable<Answer> GetAllByExamUserIdAndQuestionId(int examUserId, int QuestionId, int skip = 0, int take = 20);
    }
}
