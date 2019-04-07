using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NHT.ASM.Infrastructure;

namespace NHT.ASM.Dal
{
    /// <summary>
    /// Serves as a generic base class for concrete repositories to support basic CRUD oprations on data in the system.
    /// </summary>
    /// <typeparam name="T">The type of entity this repository works with. Must be a class inheriting <see cref="DomainEntity"/>.</typeparam>
    public class DomainEntityRepository<T> : IDomainEntityRepository<T> where T : DomainEntity
    {
        private IAsmContext Context { get; }

        protected DomainEntityRepository(IAsmContext context)
        {
            Context = context;
        }
        /// <inheritdoc />
        public virtual T FindById(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = false)
        {
            return FindAll(x => x.Id.Equals(id), include, disableTracking).SingleOrDefault();
        }

        /// <inheritdoc />
        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = false)
        {
            IQueryable<T> query = Context.Set<T>();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (include != null)
            {
                query = include(query);
            }

            return query;
        }
        
        /// <inheritdoc />
        public virtual void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        /// <inheritdoc />
        public virtual void Update(T entityInDb, T newValues)
        {
            if (newValues.IsTransient())
                newValues.Id = entityInDb.Id;
            newValues.DateCreated = entityInDb.DateCreated;
            Context.Entry(entityInDb).CurrentValues.SetValues(newValues);
        }

        /// <inheritdoc />
        public virtual void AddRange(IEnumerable<T> entities)
        {
            IEnumerable<T> enumerable = entities as IList<T> ?? entities.ToList();

            foreach (var entity in enumerable)
            {
                Add(entity);
            }
        }

        /// <inheritdoc />
        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        /// <inheritdoc />
        public virtual void Remove(int id)
        {
            var entity = FindById(id);
            Remove(entity);
        }

        /// <inheritdoc />
        public void RemoveRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
        }

        public void Dispose()
        {
            if (Context.Set<T>() != null)
            {
                Context.Dispose();
            }
        }
        /// <inheritdoc />
        public IQueryable<TResult> GetSelectList<TResult>(Expression<Func<T, TResult>> columns)
        {
            IQueryable<T> items = Context.Set<T>();

            return items.Select(columns);
        }
    }
}