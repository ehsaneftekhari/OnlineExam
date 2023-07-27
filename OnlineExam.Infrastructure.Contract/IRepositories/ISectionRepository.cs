using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Contract.IRepositories
{
    public interface ISectionRepository : IAddRepository<Section>, IGetByIdRepository<int, Section>,
        IUpdateRepository<Section>, IDeleteByIdRepository<int, Section>
    {

    }
}
