using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using NHT.ASM.Infrastructure;

namespace NHT.ASM.Bll.Interfaces
{
    /// <summary>
    /// Logic for basic Web API methods
    /// </summary>
    /// <typeparam name="TSource">Source entity</typeparam>
    /// <typeparam name="TDto">Data Transfer Object for source entity</typeparam>
    public interface ILogic<TSource, TDto> where TDto : BaseDto where TSource: DomainEntity
    {
        /// <summary>
        /// Gets an item by its unique ID.
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
        TDto GetById(int id, Func<IQueryable<TSource>, IIncludableQueryable<TSource, object>> include = null, bool disableTracking = false);
        
        /// <summary>
        /// Gets an entity by its unique ID.
        /// </summary>
        /// <param name="id">Identification field of entity</param>
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
        TSource GetEntityById(int id, Func<IQueryable<TSource>, IIncludableQueryable<TSource, object>> include = null, bool disableTracking = false);

        /// <summary>
        /// Get all the entities from the database with an predicate filter
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
        /// <returns></returns>
        IEnumerable<TDto> GetAll(Expression<Func<TSource, bool>> predicate = null, Func<IQueryable<TSource>, IIncludableQueryable<TSource, object>> include = null, bool disableTracking = false);

        /// <summary>
        /// Gets all entities from DB matching the predicate
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
        /// <returns>The requested item when found, or null otherwise.</returns>
        IQueryable<TSource> GetAllAsEntity(Expression<Func<TSource, bool>> predicate = null, Func<IQueryable<TSource>, IIncludableQueryable<TSource, object>> include = null, bool disableTracking = false);

        /// <summary>
        /// Add an <see cref="TDto"/>
        /// </summary>
        /// <param name="dtoModel">dtoModel to be added</param>
        void Post(TDto dtoModel);

        /// <summary>
        /// Add an list of  <see cref="TDto"/>
        /// </summary>
        /// <param name="dtoModels">dtoModels to be added</param>
        void PostRange(List<TDto> dtoModels);

        /// <summary>
        /// Update an <see cref="TDto"/> by id
        /// </summary>
        /// <param name="id">Id of entity to be updated</param>
        /// <param name="updatedDto">Entity with update values</param>
        void Put(int id, TDto updatedDto);

        /// <summary>
        /// Delete a(n) <see cref="TSource"/> by id
        /// </summary>
        /// <param name="id">Id of entity to be deleted</param>
        void Delete(int id);

        /// <summary>
        /// Delete an <see cref="TSource"/>
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        void Delete(TSource entity);


        /// <summary>
        /// Deletes multiple entities from the underlying collection.
        /// </summary>
        /// <param name="entities">The entity that should be deleted.</param>
        void DeleteRange(IEnumerable<TSource> entities);

        /// <summary>
        /// Used for creating Select list for UI of entity based on the columns specified for Text and Value fields.
        /// It will retrieve data only for columns which are specified
        /// </summary>
        /// <typeparam name="TResult">items of Type T</typeparam>
        /// <param name="columns">list of columns to retrieve data. For example: x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }.</param>
        /// <returns></returns>
        IEnumerable<TResult> GetSelectList<TResult>(Expression<Func<TSource, TResult>> columns);

    }
}