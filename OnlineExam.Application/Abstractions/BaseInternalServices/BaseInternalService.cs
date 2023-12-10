using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model;

namespace OnlineExam.Application.Abstractions.BaseInternalServices
{
    public abstract class BaseInternalService<TEntity, TRepository>
        : BaseInternalServiceBase<TEntity, int>
        , IBaseInternalService<TEntity, int>
        where TEntity : BaseModel
        where TRepository : IBaseRepository<TEntity>
    {
        protected readonly TRepository _repository;

        string IBaseInternalService<TEntity, int>.EntityName => EntityName;

        string IBaseInternalService<TEntity, int>.EntityIdName => EntityIdName;

        protected virtual OEApplicationException IdLessThanOneException => new ApplicationValidationException($"{EntityIdName} can not be less than 1");

        protected BaseInternalService(TRepository repository)
        {
            _repository = repository;
        }

        internal virtual IQueryable<TEntity> GetIQueryable() => _repository.GetIQueryable();

        internal virtual TEntity Add(TEntity record)
        {
            ThrowIfEntityIsNull(record);

            if (_repository.Add(record) > 0 && record.Id > 0)
                return record;

            throw DidNotAddedException;
        }

        internal virtual TEntity GetById(int id)
            => GetById(id, null);

        internal virtual TEntity GetById(int id, IQueryable<TEntity> queryable)
        {
            ThrowIfdIsNotValid(id);

            TEntity? record;

            if (queryable == null)
                record = _repository.GetById(id);
            else
                record = _repository.GetById(id, queryable);

            if (record == null && queryable == null)
                throw IsNotExistsException(id);

            if (record == null)
                throw IsNotExistsException();

            return record;
        }

        internal virtual void ThrowExceptionIfEntityIsNotExists(int entityId) 
            => GetById(entityId, null);

        internal virtual IEnumerable<TEntity> GetAll(int skip = 0, int take = 20)
        {
            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var records =
                GetIQueryable()
                .Skip(skip)
                .Take(take)
                .ToList();

            if (!records.Any())
                throw ThereIsNoEntityException;

            return records!;
        }

        internal virtual void Update(TEntity record)
        {
            ThrowIfEntityIsNull(record);

            if (_repository.Update(record) <= 0)
                throw DidNotUpdatedException;
        }

        internal virtual void Delete(int id)
            => Delete(GetById(id));

        internal virtual void Delete(TEntity record)
        {
            if (_repository.Delete(record) < 0)
                throw DidNotDeletedException;
        }

        internal virtual void ThrowIfdIsNotValid(int id)
        {
            if (id < 1)
                throw IdLessThanOneException;
        }

        IQueryable<TEntity> IBaseInternalService<TEntity, int>.GetIQueryable() => GetIQueryable();

        TEntity IBaseInternalService<TEntity, int>.Add(TEntity record) => Add(record);

        TEntity IBaseInternalService<TEntity, int>.GetById(int id) => GetById(id);

        TEntity IBaseInternalService<TEntity, int>.GetById(int id, IQueryable<TEntity> queryable) => GetById(id, queryable);

        IEnumerable<TEntity> IBaseInternalService<TEntity, int>.GetAll(int skip, int take) => GetAll(skip, take);

        void IBaseInternalService<TEntity, int>.Update(TEntity record) => Update(record);

        void IBaseInternalService<TEntity, int>.Delete(int id) => Delete(id);

        void IBaseInternalService<TEntity, int>.Delete(TEntity record)
            => Delete(record);

        void IBaseInternalService<TEntity, int>.ThrowIfdIsNotValid(int id) => ThrowIfdIsNotValid(id);

        void IBaseInternalService<TEntity, int>.ThrowIfEntityIsNull(TEntity record) => ThrowIfEntityIsNull(record);

        void IBaseInternalService<TEntity, int>.ThrowExceptionIfEntityIsNotExists(int entityId)
            => ThrowExceptionIfEntityIsNotExists (entityId);
    }
}
