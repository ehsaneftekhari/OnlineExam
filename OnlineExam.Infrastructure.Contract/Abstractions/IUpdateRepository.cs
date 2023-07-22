namespace OnlineExam.Infrastructure.Contract.Abstractions
{
    public interface IUpdateRepository<TEntity> where TEntity : class
    {
        int Update(TEntity entity);
    }
}
