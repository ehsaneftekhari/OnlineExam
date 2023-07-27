using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        OnlineExamContext _context;

        public TagRepository(OnlineExamContext context)
        {
            _context = context;
        }

        public int Add(Tag tag)
        {
            _context.Add(tag);
            return _context.SaveChanges();
        }

        public Tag? GetById(int id)
        {
            return _context.Tag.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Tag> GetIQueryable()
        {
            return _context.Tag;
        }

        public int Update(Tag tag)
        {
            _context.Tag.Update(tag);
            return _context.SaveChanges();
        }

        public int DeleteById(int id)
        {
            var tag = _context.Tag.FirstOrDefault(x => x.Id == id);
            if (tag != null)
            {
                _context.Remove(tag);
                return _context.SaveChanges();
            }
            return 0;
        }

    }
}
