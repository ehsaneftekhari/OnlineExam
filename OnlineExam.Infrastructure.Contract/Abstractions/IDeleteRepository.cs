namespace OnlineExam.Infrastructure.Contract.Abstractions
{
    public interface IDeleteRepository<TEntity> where TEntity : class
    {
        int Delete(TEntity entity);
    }
}