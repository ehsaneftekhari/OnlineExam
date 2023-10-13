using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class SectionRepository : BaseRepository<Section>, ISectionRepository
    {
        public SectionRepository(OnlineExamContext context) : base(context) { }
    }
}
