namespace OnlineExam.Infrastructure.Contract.Abstractions
{
    public interface IBaseRepository<TEntity> : IAddRepository<TEntity>, IGetRepository<TEntity, int>,
        IUpdateRepository<TEntity>, IDeleteRepository<TEntity>, IGetQueryableRepository<TEntity> where TEntity : class
    { }
}
