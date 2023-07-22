namespace OnlineExam.Infrastructure.Contract.Abstractions
{
    public interface IAddRepository<TEntity> where TEntity : class
    {
        int Add(TEntity entity);
    }
}