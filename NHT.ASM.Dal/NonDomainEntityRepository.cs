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
    /// <typeparam name="T">The type of entity this repository works with</typeparam>
    public class NonDomainEntityRepository<T> : INonDomainEntityRepository<T> where T : class
    {
        private IAsmContext Context { get; }

        /// <inheritdoc />
        public NonDomainEntityRepository(IAsmContext context)
        {
            Context = context;
            if(typeof(T).IsSubclassOf(typeof(DomainEntity)))
                throw new ArgumentException($"Type of T has inherited from {nameof(DomainEntity)} which is not allowed in {nameof(NonDomainEntityRepository<T>)}");
        }

        /// <inheritdoc />
        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        /// <inheritdoc />
        public void AddRange(IEnumerable<T> entities)
        {
            IEnumerable<T> enumerable = entities as IList<T> ?? entities.ToList();

            foreach (var entity in enumerable)
            {
                Add(entity);
            }
        }

        /// <inheritdoc />
        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = false)
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
        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
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
    }
}
