namespace OnlineExam.Infrastructure.Contract.Abstractions
{
    public interface IGetRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
    }
}
