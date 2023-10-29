namespace OnlineExam.Application.Abstractions.BaseInternalServices
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
}
