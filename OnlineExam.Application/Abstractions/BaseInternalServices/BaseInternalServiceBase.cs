using OnlineExam.Application.Contract.Exceptions;
using System.Linq.Expressions;

namespace OnlineExam.Application.Abstractions.BaseInternalServices
{

    public abstract class BaseInternalServiceBase<TEntity, TKey>
    {
        internal virtual string EntityName => typeof(TEntity).Name;

        internal virtual string EntityIdName => $"{EntityName[..1].ToLower()}{EntityName[1..]}Id";

        protected virtual OEApplicationException DidNotAddedException => new($"{EntityName} did not Added");

        protected virtual OEApplicationException ThereIsNoEntityException => new ApplicationSourceNotFoundException($"there is no {EntityName}");

        protected virtual OEApplicationException DidNotUpdatedException => new($"{EntityName} did not updated");

        protected virtual OEApplicationException DidNotDeletedException => new($"{EntityName} did not Deleted");

        public virtual OEApplicationException IsNotExistsException(TKey id) => new ApplicationSourceNotFoundException($"{EntityName} with id:{id} is not exists");

        public virtual OEApplicationException IsNotExistsException() => new ApplicationSourceNotFoundException($"{EntityName} is not exists");

        internal virtual void ThrowIfEntityIsNull(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException();
        }
    }
}
