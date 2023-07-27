namespace OnlineExam.Infrastructure.Contract.Abstractions
{
    public interface IDeleteByIdRepository<TEntity> where TEntity : class
    {
        int DeleteById(int id);
    }
}