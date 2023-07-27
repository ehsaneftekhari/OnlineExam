namespace OnlineExam.Infrastructure.Contract.Abstractions
{
    public interface IDeleteByIdRepository<TId, TEntity> where TEntity : class
    {
        int DeleteById(TId id);
    }
}