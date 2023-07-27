namespace OnlineExam.Infrastructure.Contract.Abstractions
{
    public interface IGetQueryableRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetIQueryable();
    }
}
