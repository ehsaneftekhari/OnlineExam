namespace OnlineExam.Infrastructure.Contract.Abstractions
{
    public interface IGetRepository<TEntity> where TEntity : class
    {
        public TEntity? GetById(int id);
    }
}
