namespace OnlineExam.Application.Abstractions.BaseInternalServices
{
    public interface IBaseInternalService<TEntity, TKey>
    {
        internal string EntityName { get; }

        internal string EntityIdName { get; }

        internal IQueryable<TEntity> GetIQueryable();

        internal TEntity Add(TEntity record);

        internal TEntity GetById(TKey id);

        internal TEntity GetById(TKey id, IQueryable<TEntity> queryable);

        internal void ThrowExceptionIfEntityIsNotExists(TKey entityId);

        internal IEnumerable<TEntity> GetAll(int skip = 0, int take = 20);

        internal void Update(TEntity record);

        internal void Delete(TKey id);

        internal void Delete(TEntity record);

        internal void ThrowIfdIsNotValid(TKey id);

        internal void ThrowIfEntityIsNull(TEntity record);
    }

    public interface IBaseInternalService<TEntity, TKey, TParentEntity, TParentKey>
    : IBaseInternalService<TEntity, TKey>
    {
        internal IEnumerable<TEntity> GetAllByParentId(TParentKey parentId, int skip = 0, int take = 20);
    }
    
    public interface IBaseInternalService<TEntity, TKey, TFirstParentEntity, TFirstParentKey, TSecondParentEntity, TSecondParentKey>
        : IBaseInternalService<TEntity, TKey>
    {
        internal IEnumerable<TEntity> GetAllByFirstParentId(TFirstParentKey firstParentId, int skip = 0, int take = 20);

        internal IEnumerable<TEntity> GetAllByParentsIds(TSecondParentKey firstParentId, TSecondParentKey secondParentId, int skip = 0, int take = 20);
    }
}
