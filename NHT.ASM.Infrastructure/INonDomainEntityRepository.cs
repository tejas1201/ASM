using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace NHT.ASM.Infrastructure
{
    /// <summary>
    /// Defines various methods for working with data in the system for entities not inherting <see cref="DomainEntity"/>.
    /// </summary>
    public interface INonDomainEntityRepository<T> where T : class
    {

        /// <summary>
        /// Returns an IQueryable of all items of type T.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <param name="include">Supports queryable Include/ThenInclude chaining operators.</param>
        /// <param name="disableTracking">
        ///     <para><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</para>
        ///     <para>
        ///         Returns a new query where the change tracker will not track any of the entities that are returned.
        ///         If the entity instances are modified, this will not be detected by the change tracker and
        ///         <see cref="M:Microsoft.EntityFrameworkCore.DbContext.SaveChanges" /> will not persist those changes to the database.
        ///     </para>
        ///     <para>
        ///         Disabling change tracking is useful for read-only scenarios because it avoids the overhead of setting
        ///         up change tracking for each entity instance. You should not disable change tracking if you want to
        ///         manipulate entity instances and persist those changes to the database using
        ///         <see cref="M:Microsoft.EntityFrameworkCore.DbContext.SaveChanges" />.
        ///     </para>
        ///     <para>
        ///         Identity resolution will still be performed to ensure that all occurrences of an entity with a given key
        ///         in the result set are represented by the same entity instance.
        ///     </para>
        /// </param>
        /// <returns>An IQueryable of the requested type T.</returns>
        /// <remarks>https://stackoverflow.com/a/47039696/1343595</remarks>
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = false);

        /// <summary>
        /// Adds an entity to the underlying collection.
        /// </summary>
        /// <param name="entity">The entity that should be added.</param>
        void Add(T entity);

        /// <summary>
        /// Adds multiple entities to the underlying DbContext
        /// </summary>
        /// <param name="entities">The entities that should be added.</param>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// Removes an entity from the underlying collection.
        /// </summary>
        /// <param name="entity">The entity that should be removed.</param>
        void Remove(T entity);

        /// <summary>
        /// Removes multiple entities from the underlying collection.
        /// </summary>
        /// <param name="entities">The entity that should be removed.</param>
        void RemoveRange(IEnumerable<T> entities);
    }
}
