using Castle.DynamicProxy;
using OnlineExam.Application.AttributeManagers;
using OnlineExam.Infrastructure.Contract.IUnitOfWorks;

namespace OnlineExam.Application.Inteceptors
{
    internal class TransactionUnitOfWorkInterceptor : IInterceptor
    {
        readonly TransactionUnitOfWorkAttributeManager attributeManager;
        readonly ITransactionUnitOfWork transactionUnitOfWork;

        public TransactionUnitOfWorkInterceptor(TransactionUnitOfWorkAttributeManager attributeManager, ITransactionUnitOfWork transactionUnitOfWork)
        {
            this.attributeManager = attributeManager;
            this.transactionUnitOfWork = transactionUnitOfWork;
        }

        public void Intercept(IInvocation invocation)
        {
            if(attributeManager.HasMethod(invocation.MethodInvocationTarget))
            {
                try
                {
                    transactionUnitOfWork.Begin();
                    invocation.Proceed();
                    transactionUnitOfWork.Commit();
                }
                catch
                {
                    //todo:log
                    transactionUnitOfWork.Rollback();

                    throw;
                }
            }
            else
                invocation.Proceed();
        }
    }
}
