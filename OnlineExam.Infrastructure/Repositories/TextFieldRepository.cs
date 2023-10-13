using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class TextFieldRepository : BaseRepository<TextField>, ITextFieldRepository
    {
        public TextFieldRepository(OnlineExamContext context) : base(context) { }
    }
}
