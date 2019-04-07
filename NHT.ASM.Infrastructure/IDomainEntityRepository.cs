using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NHT.ASM.Infrastructure
{
    /// <summary>
    /// Defines various methods for working with data in the system.
    /// </summary>

    public interface IDomainEntityRepository<T> where T : DomainEntity
    {
        /// <summary>
        /// Finds an item by its unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the item in the database.</param>
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
        /// <returns>The requested item when found, or null otherwise.</returns>
        T FindById(int id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = false);

        
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
        /// Adds an entity to the underlying Db DataContextFactory.GetDataContext().
        /// </summary>
        /// <param name="entityInDb">The entity that should be updated</param>
        /// <param name="newValues">The values to update with</param>
        void Update(T entityInDb, T newValues);

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
        /// Removes an entity from the underlying collection.
        /// </summary>
        /// <param name="id">The ID of the entity that should be removed.</param>
        void Remove(int id);

        /// <summary>
        /// Removes multiple entities from the underlying collection.
        /// </summary>
        /// <param name="entities">The entity that should be removed.</param>
        void RemoveRange(IEnumerable<T> entities);

        /// <summary>
        /// Used for creating Select list for UI of entity based on the columns specified for Text and Value fields.
        /// It will retrieve data only for columns which are specified
        /// </summary>
        /// <typeparam name="TResult">items of Type T</typeparam>
        /// <param name="columns">list of columns to retrieve data</param>
        /// <returns></returns>
        IQueryable<TResult> GetSelectList<TResult>(Expression<Func<T, TResult>> columns);
    }
}