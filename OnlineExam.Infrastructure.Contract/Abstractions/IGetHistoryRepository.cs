namespace OnlineExam.Infrastructure.Contract.Abstractions
{
    public interface IGetHistoryRepository<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> GetAllById(int id);
    }
}
