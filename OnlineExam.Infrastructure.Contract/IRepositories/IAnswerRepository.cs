using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model.Models;
using System.Security.Cryptography;

namespace OnlineExam.Infrastructure.Contract.IRepositories
{
    public interface IAnswerRepository : IAddRepository<Answer>, IGetRepository<Answer, (int ExamUserId, int QuestionId)>, 
        IUpdateRepository<Answer>, IDeleteRepository<Answer>, IGetQueryableRepository<Answer>
    {

        Answer? GetById(int ExamUserId, int QuestionId);
    }
}
