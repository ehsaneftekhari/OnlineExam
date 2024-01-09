namespace OnlineExam.Infrastructure.Contract.Abstractions
{
    public interface IGetRepository<TEntity, TId> where TEntity : class
    {
        TEntity? GetById(TId id);
        TEntity? GetById(TId id, IQueryable<TEntity> queryable);
    }
}
