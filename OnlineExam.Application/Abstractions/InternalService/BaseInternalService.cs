using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace OnlineExam.Application.Abstractions.InternalService
{
    public abstract class BaseInternalService<TEntity, TRepository>
        where TEntity : BaseModel
        where TRepository : IBaseRepository<TEntity>
    {
        protected readonly TRepository _repository;

        protected BaseInternalService(TRepository repository)
        {
            _repository = repository;
        }

        internal virtual string EntityName => nameof(TEntity);

        internal virtual string EntityIdName => $"{EntityName.Substring(0, 1).ToLower()}{EntityName.Substring(1)}Id";

        protected virtual OEApplicationException DidNotAddedException => new OEApplicationException($"{EntityName} did not Added");

        protected virtual OEApplicationException ThereIsNoEntityException => new ApplicationSourceNotFoundException($"there is no {EntityName}");

        protected virtual OEApplicationException DidNotUpdatedException => new OEApplicationException($"{EntityName} did not updated");

        protected virtual OEApplicationException DidNotDeletedException => new OEApplicationException($"{EntityName} did not Deleted");

        protected virtual OEApplicationException IdLessThanOneException => new ApplicationValidationException($"{EntityIdName} can not be less than 1");

        protected virtual OEApplicationException IsNotExistsException(int id) => new ApplicationSourceNotFoundException($"{EntityName} with id:{id} is not exists");

        internal virtual TEntity Add(TEntity record)
        {
            ThrowIfEntityIsNull(record);

            if (_repository.Add(record) > 0 && record.Id > 0)
                return record;

            throw DidNotAddedException;
        }

        internal virtual TEntity GetById(int id)
        {
            ThrowIfdIsNotValid(id);

            var records = _repository.GetById(id);

            if (records == null)
                throw IsNotExistsException(id);

            return records;
        }

        internal virtual void ThrowExceptionIfEntityIsNotExists(int entityId) => GetById(entityId);

        internal virtual IEnumerable<TEntity> GetAllByQuestionId(int skip = 0, int take = 20)
        {
            if (skip < 0 || take < 1)
                throw new OEApplicationException();

            var records =
                _repository.GetIQueryable()
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
        {
            var textField = GetById(id);

            if (_repository.Delete(textField) < 0)
                throw DidNotDeletedException;
        }

        internal virtual void ThrowIfdIsNotValid(int textFieldId)
        {
            if (textFieldId < 1)
                throw IdLessThanOneException;
        }

        internal virtual void ThrowIfEntityIsNull(TEntity textField)
        {
            if (textField == null)
                throw new ArgumentNullException();
        }
    }


    public abstract class BaseInternalService<TEntity, TRepository, TParentEntity, TParentRepository> : BaseInternalService<TEntity, TRepository>
        where TEntity : BaseModel
        where TRepository : IBaseRepository<TEntity>
        where TParentEntity : BaseModel
        where TParentRepository : IBaseRepository<TParentEntity>
    {
        protected readonly BaseInternalService<TParentEntity, TParentRepository> _parentInternalService;

        public BaseInternalService(TRepository repository,
                                      BaseInternalService<TParentEntity, TParentRepository> parentInternalService) : base(repository)
        {
            _parentInternalService = parentInternalService;
        }

        protected abstract Expression<Func<TEntity, int>> ParentIdProvider { get; }

        protected virtual OEApplicationException ThereIsNoEntityException(int parentId)
            => new ApplicationSourceNotFoundException($"there is no {EntityName} within {_parentInternalService.EntityName} (id:{parentId})");

        protected IQueryable<TEntity> GetIQueryable() => _repository.GetIQueryable();

        internal virtual TEntity Add(TEntity entity)
        {
            try
            {
                return base.Add(entity);
            }
            catch
            {
                _parentInternalService.ThrowExceptionIfEntityIsNotExists(ParentIdProvider.Compile().Invoke(entity));

                throw;
            }
        }

        internal virtual IEnumerable<TEntity> GetAllByParentId(int parentId, int skip = 0, int take = 20)
        {
            _parentInternalService.ThrowIfdIsNotValid(parentId);

            if (skip < 0 || take < 1)
                throw new OEApplicationException();


            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var idComparison = Expression.Equal(Expression.Invoke(ParentIdProvider, parameter), Expression.Constant(parentId));
            var predicate = Expression.Lambda<Func<TEntity, bool>>(idComparison, parameter);
            
            var textFields = GetIQueryable()
                .Where(predicate)
                .Skip(skip)
                .Take(take)
                .ToList();

            if (!textFields.Any())
            {
                _parentInternalService.ThrowExceptionIfEntityIsNotExists(parentId);

                throw ThereIsNoEntityException(parentId);
            }

            return textFields!;
        }

        internal virtual void ThrowIfEntityIsNull(TEntity entity)
        {
            _parentInternalService.ThrowIfdIsNotValid(ParentIdProvider.Compile().Invoke(entity));

            if (entity == null)
                throw new ArgumentNullException();
        }
    }
}
