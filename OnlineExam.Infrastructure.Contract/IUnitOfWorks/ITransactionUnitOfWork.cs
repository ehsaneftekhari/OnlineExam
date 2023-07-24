namespace OnlineExam.Infrastructure.Contract.IUnitOfWorks
{
    public interface ITransactionUnitOfWork
    {
        void Begin();
        void Commit();
        void Rollback();
    }
}
