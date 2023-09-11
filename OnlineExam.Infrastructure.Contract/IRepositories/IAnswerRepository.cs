using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model.Models;
using System.Security.Cryptography;

namespace OnlineExam.Infrastructure.Contract.IRepositories
{
    public interface IAnswerRepository : IAddRepository<Answer>, IGetRepository<Answer, int>, 
        IUpdateRepository<Answer>, IDeleteRepository<Answer>, IGetQueryableRepository<Answer>
    {
    }
}
