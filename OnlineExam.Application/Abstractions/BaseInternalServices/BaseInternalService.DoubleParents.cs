using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model;
using System.Linq.Expressions;

namespace OnlineExam.Application.Abstractions.BaseInternalServices
{
    public abstract class BaseInternalService<TEntity, TRepository, TFirstParentEntity,
            TFirstParentRepository, TSecondParentEntity, TSecondParentRepository>

        : BaseInternalService<TEntity, TRepository>
        , IBaseInternalService<TEntity, int, TFirstParentEntity, int, TSecondParentEntity, int>

        where TEntity : BaseModel
        where TRepository : IBaseRepository<TEntity>
        where TFirstParentEntity : BaseModel
        where TFirstParentRepository : IBaseRepository<TFirstParentEntity>
        where TSecondParentEntity : BaseModel
        where TSecondParentRepository : IBaseRepository<TSecondParentEntity>
    {
        protected readonly IBaseInternalService<TFirstParentEntity, int> _firstParentInternalService;
        protected readonly IBaseInternalService<TSecondParentEntity, int> _secondParentInternalService;

        public BaseInternalService(TRepository repository,
                                   IBaseInternalService<TFirstParentEntity, int> firstParentInternalService,
                                   IBaseInternalService<TSecondParentEntity, int> secondParentInternalService) : base(repository)
        {
            _firstParentInternalService = firstParentInternalService;
            _secondParentInternalService = secondParentInternalService;
        }

        protected abstract Expression<Func<TEntity, int>> FirstParentIdProvider { get; }

        protected abstract Expression<Func<TEntity, int>> SecondParentIdProvider { get; }

        protected OEApplicationException ThereIsNoEntityInFirstParentException(int parentId)
            => new ApplicationSourceNotFoundException($"there is no {EntityName} within {_firstParentInternalService.EntityName} ({_firstParentInternalService.EntityIdName}:{parentId})");

        protected new OEApplicationException ThereIsNoEntityException(int firstParentId, int secondParentId)
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

        internal override void ThrowIfEntityIsNull(TEntity record)
        {
            _firstParentInternalService.ThrowIfdIsNotValid(FirstParentIdProvider.Compile().Invoke(record));
            _secondParentInternalService.ThrowIfdIsNotValid(SecondParentIdProvider.Compile().Invoke(record));

            base.ThrowIfEntityIsNull(record);
        }

        IEnumerable<TEntity> IBaseInternalService<TEntity, int, TFirstParentEntity, int, TSecondParentEntity, int>.GetAllByFirstParentId(int firstParentId, int skip, int take)
            => GetAllByFirstParentId(firstParentId, skip, take);

        IEnumerable<TEntity> IBaseInternalService<TEntity, int, TFirstParentEntity, int, TSecondParentEntity, int>.GetAllByParentsIds(int firstParentId, int secondParentId, int skip, int take)
            => GetAllByParentsIds(firstParentId, firstParentId, skip, take);
    }
}
