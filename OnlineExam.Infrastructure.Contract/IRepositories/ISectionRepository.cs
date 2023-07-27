using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Contract.IRepositories
{
    public interface ISectionRepository : IAddRepository<Section>, IGetRepository<Section>,
        IUpdateRepository<Section>, IDeleteByIdRepository<Section>
    {

    }
}
