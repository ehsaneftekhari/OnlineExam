namespace OnlineExam.Infrastructure.Contract.Abstractions
{
    public interface IGetByIdRepository<TId,TEntity> where TEntity : class
    {
        public TEntity? GetById(TId id);
    }
}
