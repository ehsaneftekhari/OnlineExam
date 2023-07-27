namespace OnlineExam.Infrastructure.Contract.Abstractions
{
    public interface IDeleteByEntityRepository<TEntity> where TEntity : class
    {
        int DeleteByEntity(TEntity entity);
    }
}