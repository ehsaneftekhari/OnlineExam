using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model;
using System.Linq.Expressions;

namespace OnlineExam.Application.Abstractions.InternalService
{
    public interface IBaseInternalService<TEntity>
    {
        internal string EntityName { get; }

        internal string EntityIdName { get; }

        internal IQueryable<TEntity> GetIQueryable();

        internal TEntity Add(TEntity record);

        internal TEntity GetById(int id);

        internal TEntity GetById(int id, IQueryable<TEntity> queryable);

        internal void ThrowExceptionIfEntityIsNotExists(int entityId) => GetById(entityId);

        internal IEnumerable<TEntity> GetAll(int skip = 0, int take = 20);

        internal void Update(TEntity record);

        internal void Delete(int id);

        internal void ThrowIfdIsNotValid(int id);

        internal void ThrowIfEntityIsNotValid(TEntity record);
    }

    public interface IBaseInternalService<TEntity, TParentEntity>
        : IBaseInternalService<TEntity>
    {
        internal IEnumerable<TEntity> GetAllByParentId(int parentId, int skip = 0, int take = 20);
    }

    public interface IBaseInternalService<TEntity, TFirstParentEntity, TSecondParentEntity>
        : IBaseInternalService<TEntity>
    {
        internal IEnumerable<TEntity> GetAllByFirstParentId(int firstParentId, int skip = 0, int take = 20);

        internal IEnumerable<TEntity> GetAllByParentsIds(int firstParentId, int secondParentId, int skip = 0, int take = 20);
    }

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


    public abstract class BaseInternalService<TEntity, TRepository, TParentEntity, TParentRepository>

        : BaseInternalService<TEntity, TRepository>
        , IBaseInternalService<TEntity, TParentEntity>

        where TEntity : BaseModel
        where TRepository : IBaseRepository<TEntity>
        where TParentEntity : BaseModel
        where TParentRepository : IBaseRepository<TParentEntity>
    {
        protected readonly IBaseInternalService<TParentEntity> _parentInternalService;

        public BaseInternalService(TRepository repository,
                                   IBaseInternalService<TParentEntity> parentInternalService) : base(repository)
        {
            _parentInternalService = parentInternalService;
        }

        protected abstract Expression<Func<TEntity, int>> ParentIdProvider { get; }

        protected OEApplicationException ThereIsNoEntityException(int parentId)
            => new ApplicationSourceNotFoundException($"there is no {EntityName} within {_parentInternalService.EntityName} ({_parentInternalService.EntityIdName}:{parentId})");

        internal override TEntity Add(TEntity newRecord)
        {
            try
            {
                return base.Add(newRecord);
            }
            catch
            {
                _parentInternalService.ThrowExceptionIfEntityIsNotExists(ParentIdProvider.Compile().Invoke(newRecord));

                throw;
            }
        }

        internal IEnumerable<TEntity> GetAllByParentId(int parentId, int skip = 0, int take = 20)
        {
            _parentInternalService.ThrowIfdIsNotValid(parentId);

            if (skip < 0 || take < 1)
                throw new OEApplicationException();


            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var idComparison = Expression.Equal(Expression.Invoke(ParentIdProvider, parameter), Expression.Constant(parentId));
            var predicate = Expression.Lambda<Func<TEntity, bool>>(idComparison, parameter);

            var records = GetIQueryable()
                .Where(predicate)
                .Skip(skip)
                .Take(take)
                .ToList();

            if (!records.Any())
            {
                _parentInternalService.ThrowExceptionIfEntityIsNotExists(parentId);

                throw ThereIsNoEntityException(parentId);
            }

            return records!;
        }

        internal override void ThrowIfEntityIsNotValid(TEntity record)
        {
            _parentInternalService.ThrowIfdIsNotValid(ParentIdProvider.Compile().Invoke(record));

            base.ThrowIfEntityIsNotValid(record);
        }

        IEnumerable<TEntity> IBaseInternalService<TEntity, TParentEntity>.GetAllByParentId(int parentId, int skip, int take) => GetAllByParentId(parentId, skip, take);
    }

    public abstract class BaseInternalService<TEntity, TRepository, TFirstParentEntity,
            TFirstParentRepository, TSecondParentEntity, TSecondParentRepository>

        : BaseInternalService<TEntity, TRepository>
        , IBaseInternalService<TEntity, TFirstParentEntity, TSecondParentEntity>

        where TEntity : BaseModel
        where TRepository : IBaseRepository<TEntity>
        where TFirstParentEntity : BaseModel
        where TFirstParentRepository : IBaseRepository<TFirstParentEntity>
        where TSecondParentEntity : BaseModel
        where TSecondParentRepository : IBaseRepository<TSecondParentEntity>
    {
        protected readonly IBaseInternalService<TFirstParentEntity> _firstParentInternalService;
        protected readonly IBaseInternalService<TSecondParentEntity> _secondParentInternalService;

        public BaseInternalService(TRepository repository,
                                   IBaseInternalService<TFirstParentEntity> firstParentInternalService,
                                   IBaseInternalService<TSecondParentEntity> secondParentInternalService) : base(repository)
        {
            _firstParentInternalService = firstParentInternalService;
            _secondParentInternalService = secondParentInternalService;
        }

        protected abstract Expression<Func<TEntity, int>> FirstParentIdProvider { get; }

        protected abstract Expression<Func<TEntity, int>> SecondParentIdProvider { get; }

        protected OEApplicationException ThereIsNoEntityInFirstParentException(int parentId)
            => new ApplicationSourceNotFoundException($"there is no {EntityName} within {_firstParentInternalService.EntityName} ({_firstParentInternalService.EntityIdName}:{parentId})");

        protected OEApplicationException ThereIsNoEntityException(int firstParentId, int secondParentId)
            => new ApplicationSourceNotFoundException($"there is no {EntityName} for {_firstParentInternalService.EntityName} ({_firstParentInternalService.EntityIdName}:{firstParentId}) and {_secondParentInternalService.EntityName} ({_secondParentInternalService.EntityIdName}:{secondParentId})");

        internal override TEntity Add(TEntity newRecord)
        {
            try
            {
                return base.Add(newRecord);
            }
            catch
            {
                _firstParentInternalService.ThrowExceptionIfEntityIsNotExists(FirstParentIdProvider.Compile().Invoke(newRecord));
                _secondParentInternalService.ThrowExceptionIfEntityIsNotExists(SecondParentIdProvider.Compile().Invoke(newRecord));

                throw;
            }
        }

        internal IEnumerable<TEntity> GetAllByFirstParentId(int firstParentId, int skip = 0, int take = 20)
        {
            _firstParentInternalService.ThrowIfdIsNotValid(firstParentId);

            if (skip < 0 || take < 1)
                throw new OEApplicationException();


            var parameter = Expression.Parameter(typeof(TEntity), "e");

            var firstParentIdComparison = Expression.Equal(Expression.Invoke(FirstParentIdProvider, parameter), Expression.Constant(firstParentId));
            var firstParentPredicate = Expression.Lambda<Func<TEntity, bool>>(firstParentIdComparison, parameter);

            var records = GetIQueryable()
                .Where(firstParentPredicate)
                .Skip(skip)
                .Take(take)
                .ToList();

            if (!records.Any())
            {
                _firstParentInternalService.ThrowExceptionIfEntityIsNotExists(firstParentId);

                throw ThereIsNoEntityInFirstParentException(firstParentId);
            }

            return records!;
        }

        internal IEnumerable<TEntity> GetAllByParentsIds(int firstParentId, int secondParentId, int skip = 0, int take = 20)
        {
            _firstParentInternalService.ThrowIfdIsNotValid(firstParentId);
            _secondParentInternalService.ThrowIfdIsNotValid(secondParentId);

            if (skip < 0 || take < 1)
                throw new OEApplicationException();


            var parameter = Expression.Parameter(typeof(TEntity), "e");

            var firstParentIdComparison = Expression.Equal(Expression.Invoke(FirstParentIdProvider, parameter), Expression.Constant(firstParentId));
            var firstParentPredicate = Expression.Lambda<Func<TEntity, bool>>(firstParentIdComparison, parameter);

            var secondParentIdComparison = Expression.Equal(Expression.Invoke(SecondParentIdProvider, parameter), Expression.Constant(secondParentId));
            var secondParentPredicate = Expression.Lambda<Func<TEntity, bool>>(secondParentIdComparison, parameter);

            var records = GetIQueryable()
                .Where(firstParentPredicate)
                .Where(secondParentPredicate)
                .Skip(skip)
                .Take(take)
                .ToList();

            if (!records.Any())
            {
                _firstParentInternalService.ThrowExceptionIfEntityIsNotExists(firstParentId);
                _secondParentInternalService.ThrowExceptionIfEntityIsNotExists(secondParentId);


                throw ThereIsNoEntityException(firstParentId, secondParentId);
            }

            return records!;
        }

        internal override void ThrowIfEntityIsNotValid(TEntity record)
        {
            _firstParentInternalService.ThrowIfdIsNotValid(FirstParentIdProvider.Compile().Invoke(record));
            _secondParentInternalService.ThrowIfdIsNotValid(SecondParentIdProvider.Compile().Invoke(record));

            base.ThrowIfEntityIsNotValid(record);
        }

        IEnumerable<TEntity> IBaseInternalService<TEntity, TFirstParentEntity, TSecondParentEntity>.GetAllByFirstParentId(int firstParentId, int skip, int take)
            => GetAllByFirstParentId(firstParentId, skip, take);

        IEnumerable<TEntity> IBaseInternalService<TEntity, TFirstParentEntity, TSecondParentEntity>.GetAllByParentsIds(int firstParentId, int secondParentId, int skip, int take)
            => GetAllByParentsIds(firstParentId, firstParentId, skip, take);
    }
}
