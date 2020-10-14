using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BusinessService.Domain.Services
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Interface method for getting the customer data
        /// </summary>

        TEntity Get(int? id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Interface method for adding the customer data
        /// </summary>

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Interface method for removing the customer data
        /// </summary>

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

    }
}
