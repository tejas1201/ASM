using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using NHT.ASM.Bll.ConfigurationHelpers;
using NHT.ASM.Bll.Interfaces;
using NHT.ASM.Dal.Uow;
using NHT.ASM.Infrastructure;

namespace NHT.ASM.Bll.Logic
{
    /// <summary>
    /// Base class for logic
    /// </summary>
    /// <typeparam name="TSource">Entity from database</typeparam>
    /// <typeparam name="TDto">Data Transfer Object to transfer data to the client</typeparam>
    public abstract class BaseLogic<TSource,TDto> : ILogic<TSource,TDto> where TSource:DomainEntity where TDto : BaseDto
    {
        private readonly IAsmContext _context;
        private readonly IDomainEntityRepository<TSource> _repository;

        //ToDo: Add error handling

        protected BaseLogic(IAsmContext context, IDomainEntityRepository<TSource> repository)
        {
            _context = context;
            _repository = repository;
        }
        
        
        /// <inheritdoc />
        public IEnumerable<TDto> GetAll(Expression<Func<TSource, bool>> predicate = null,
            Func<IQueryable<TSource>, IIncludableQueryable<TSource, object>> include = null, bool disableTracking = false)
        {
            IQueryable<TSource> models = _repository.FindAll(predicate, include, disableTracking);

            var dto = models.MapIQueryableToListOf<TDto>();
            return dto;
        }
        
        /// <inheritdoc />
        public TDto GetById(int id, Func<IQueryable<TSource>, IIncludableQueryable<TSource, object>> include = null, bool disableTracking = false)
        {
            TSource model = _repository.FindById(id, include, disableTracking);

            return model.MapTo<TDto>();
        }

        /// <inheritdoc />
        public TSource GetEntityById(int id, Func<IQueryable<TSource>, IIncludableQueryable<TSource, object>> include = null, bool disableTracking = false)
        {
            return _repository.FindById(id, include, disableTracking);
        }

        /// <inheritdoc />
        public IQueryable<TSource> GetAllAsEntity(Expression<Func<TSource, bool>> predicate = null, Func<IQueryable<TSource>, IIncludableQueryable<TSource, object>> include = null, bool disableTracking = false)
        {
            return _repository.FindAll(predicate, include, disableTracking);
        }
        
        /// <inheritdoc />
        public virtual void Post(TDto dtoModel)
        {
            using (new EfUnitOfWorkFactory().Create(_context))
            {
                _repository.Add(dtoModel.MapTo<TSource>());
            }
        }

        /// <inheritdoc />
        public void PostRange(List<TDto> dtoModels)
        {
            using (new EfUnitOfWorkFactory().Create(_context))
            {
                _repository.AddRange(dtoModels.MapListToListOf<TSource>());
            }
        }

        /// <inheritdoc />
        public virtual void Put(int id, TDto updatedDto)
        {
            using (new EfUnitOfWorkFactory().Create(_context))
            {
                TSource model = _repository.FindById(id);
                _repository.Update(model, updatedDto.MapTo<TSource>());
            }
        }
        
        /// <inheritdoc />
        public virtual void Delete(int id)
        {
            using (new EfUnitOfWorkFactory().Create(_context))
            {
                _repository.Remove(id);
            }
        }

        /// <inheritdoc />
        public virtual void Delete(TSource entity)
        {
            using (new EfUnitOfWorkFactory().Create(_context))
            {
                _repository.Remove(entity);
            }
        }

        /// <inheritdoc />
        public void DeleteRange(IEnumerable<TSource> entities)
        {
            using (new EfUnitOfWorkFactory().Create(_context))
            {
                _repository.RemoveRange(entities);
            }
        }

        /// <inheritdoc />
        public IEnumerable<TResult> GetSelectList<TResult>(Expression<Func<TSource, TResult>> columns)
        {
           return _repository.GetSelectList(columns);
        }
    }
}