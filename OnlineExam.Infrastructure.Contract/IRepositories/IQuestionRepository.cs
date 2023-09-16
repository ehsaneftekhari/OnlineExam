using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Contract.IRepositories
{
    public interface IQuestionRepository : IAddRepository<Question>, IGetRepository<Question, int>
        , IUpdateRepository<Question>, IDeleteRepository<Question>, IGetQueryableRepository<Question>
    {
    }
}
