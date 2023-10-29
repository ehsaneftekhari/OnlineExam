using OnlineExam.Model.Models;

namespace OnlineExam.Application.Abstractions.InternalService
{
    public interface IAnswerInternalService : IBaseInternalService<Answer, ExamUser, Question>
    {
        internal IEnumerable<Answer> GetAllByExamUserId(int examUserId, int skip = 0, int take = 20);
        internal IEnumerable<Answer> GetAllByExamUserIdAndQuestionId(int examUserId, int QuestionId, int skip = 0, int take = 20);
    }
}
