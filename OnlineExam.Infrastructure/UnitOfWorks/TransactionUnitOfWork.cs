using Microsoft.EntityFrameworkCore.Storage;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IUnitOfWorks;

namespace OnlineExam.Infrastructure.UnitOfWorks
{
    internal class TransactionUnitOfWork : ITransactionUnitOfWork
    {
        OnlineExamContext _context;
        IDbContextTransaction _transaction;

        public TransactionUnitOfWork(OnlineExamContext context)
        {
            _context = context;
        }

        public void Begin()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}
