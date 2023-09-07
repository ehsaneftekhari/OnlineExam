﻿using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.Abstractions;
using OnlineExam.Model;

namespace OnlineExam.Infrastructure.Abstraction
{
    public abstract class BaseRepository<TEntity> : IAddRepository<TEntity>, IGetRepository<TEntity>,
        IUpdateRepository<TEntity>, IDeleteRepository<TEntity>, IGetQueryableRepository<TEntity> where TEntity : BaseModel
    {
        protected readonly OnlineExamContext _context;

        public BaseRepository(OnlineExamContext context)
        {
            _context = context;
        }

        public virtual int Add(TEntity question)
        {
            _context.Set<TEntity>().Add(question);
            return _context.SaveChanges();
        }

        public virtual int Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return _context.SaveChanges();
        }

        public virtual TEntity? GetById(int id)
        {
            return _context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
        }

        public virtual IQueryable<TEntity> GetIQueryable()
        {
            return _context.Set<TEntity>();
        }

        public virtual int Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return _context.SaveChanges();
        }
    }
}