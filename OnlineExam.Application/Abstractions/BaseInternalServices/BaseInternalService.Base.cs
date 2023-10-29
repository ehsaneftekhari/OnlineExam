using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model;
using System.Linq.Expressions;

namespace OnlineExam.Application.Abstractions.BaseInternalServices
{

    public abstract class BaseInternalService<TEntity>
    {
        internal virtual string EntityName => typeof(TEntity).Name;

        internal virtual string EntityIdName => $"{EntityName.Substring(0, 1).ToLower()}{EntityName.Substring(1)}Id";

        protected virtual OEApplicationException DidNotAddedException => new OEApplicationException($"{EntityName} did not Added");

        protected virtual OEApplicationException ThereIsNoEntityException => new ApplicationSourceNotFoundException($"there is no {EntityName}");

        protected virtual OEApplicationException DidNotUpdatedException => new OEApplicationException($"{EntityName} did not updated");

        protected virtual OEApplicationException DidNotDeletedException => new OEApplicationException($"{EntityName} did not Deleted");

        protected virtual OEApplicationException IdLessThanOneException => new ApplicationValidationException($"{EntityIdName} can not be less than 1");

        protected virtual OEApplicationException IsNotExistsException(int id) => new ApplicationSourceNotFoundException($"{EntityName} with id:{id} is not exists");

        internal virtual void ThrowIfdIsNotValid(int id)
        {
            if (id < 1)
                throw IdLessThanOneException;
        }

        internal virtual void ThrowIfEntityIsNotValid(TEntity record)
        {
            if (record == null)
                throw new ArgumentNullException();
        }
    }

    public abstract class BaseInternalService<TEntity, TRepository>
        : BaseInternalService<TEntity>
        , IBaseInternalService<TEntity>
        where TEntity : BaseModel
        where TRepository : IBaseRepository<TEntity>
    {
        protected readonly TRepository _repository;

        string IBaseInternalService<TEntity>.EntityName => EntityName;

        string IBaseInternalService<TEntity>.EntityIdName => EntityIdName;

        protected BaseInternalService(TRepository repository)
        {
            _repository = repository;
        }

        internal virtual IQueryable<TEntity> GetIQueryable() => _repository.GetIQueryable();

        internal virtual TEntity Add(TEntity record)
        {
            ThrowIfEntityIsNotValid(record);

            if (_repository.Add(record) > 0 && record.Id > 0)
                return record;

            throw DidNotAddedException;
        }

        internal virtual TEntity GetById(int id)
            => GetById(id, null);

        internal virtual TEntity GetById(int id, IQueryable<TEntity> queryable)
        {
            ThrowIfdIsNotValid(id);

            TEntity records;

            if (queryable == null)
                records = _repository.GetById(id);
            else
                records = _repository.GetById(id, queryable);

            if (records == null)
                throw IsNotExistsException(id);

            return records;
        }

        internal virtual void ThrowExceptionIfEntityIsNotExists(int entityId) => GetById(entityId);

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
            ThrowIfEntityIsNotValid(record);

            if (_repository.Update(record) <= 0)
                throw DidNotUpdatedException;
        }

        internal virtual void Delete(int id)
        {
            var textField = GetById(id);

            if (_repository.Delete(textField) < 0)
                throw DidNotDeletedException;
        }

        IQueryable<TEntity> IBaseInternalService<TEntity>.GetIQueryable() => GetIQueryable();

        TEntity IBaseInternalService<TEntity>.Add(TEntity record) => Add(record);

        TEntity IBaseInternalService<TEntity>.GetById(int id) => GetById(id);

        TEntity IBaseInternalService<TEntity>.GetById(int id, IQueryable<TEntity> queryable) => GetById(id, queryable);

        IEnumerable<TEntity> IBaseInternalService<TEntity>.GetAll(int skip, int take) => GetAll(skip, take);

        void IBaseInternalService<TEntity>.Update(TEntity record) => Update(record);

        void IBaseInternalService<TEntity>.Delete(int id) => Delete(id);

        void IBaseInternalService<TEntity>.ThrowIfdIsNotValid(int id) => ThrowIfdIsNotValid(id);

        void IBaseInternalService<TEntity>.ThrowIfEntityIsNotValid(TEntity record) => ThrowIfEntityIsNotValid(record);
    }
}
