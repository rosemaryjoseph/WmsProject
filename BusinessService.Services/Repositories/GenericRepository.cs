using BusinessService.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessService.Services.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private DbSet<TEntity> _table;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _table.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _table.AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _table.Where(predicate);
        }

        public TEntity Get(int? id)
        {
            return _table.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _table.ToList();
        }

        public void Remove(TEntity entity)
        {
            _table.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _table.RemoveRange(entities);
        }
        public virtual void Update(TEntity entity)
        {
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
