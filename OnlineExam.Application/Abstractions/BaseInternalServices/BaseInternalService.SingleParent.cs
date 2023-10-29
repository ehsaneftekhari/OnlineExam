using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model;
using System.Linq.Expressions;

namespace OnlineExam.Application.Abstractions.BaseInternalServices
{
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
}
