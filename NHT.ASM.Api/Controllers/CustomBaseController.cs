using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NHT.ASM.Api.ActionResults;
using NHT.ASM.Bll.Interfaces;
using NHT.ASM.Helpers.ExtensionMethods;
using NHT.ASM.Infrastructure;

namespace NHT.ASM.Api.Controllers
{
    /// <summary>
    /// Base controller to inherit base Web API controller logic from
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    [SuppressMessage("ReSharper", "InheritdocConsiderUsage")]
    public class CustomBaseController<TSource, TDto> : CustomErrorHandlingController where TDto : BaseDto where TSource : DomainEntity
    {
        /// <summary>
        /// The logic containing all business rules used in the controllers
        /// </summary>
        protected readonly ILogic<TSource, TDto> Logic;

        /// <summary>
        /// The Database context used in the controllers
        /// </summary>
        protected readonly IAsmContext Context;

        /// <summary>
        /// Instantiates the base controller
        /// </summary>
        /// <param name="logic"></param>
        /// <param name="context"></param>
        /// <param name="hostingEnvironment"></param>
        protected CustomBaseController(ILogic<TSource, TDto> logic, IAsmContext context, IHostingEnvironment hostingEnvironment)
            : base(hostingEnvironment)
        {
            Logic = logic;
            Context = context;
        }

        /// <summary>
        /// Creates Select list for UI of entity based on the columns specified for Text and Value fields.
        /// It will retrieve data only for columns which are specified
        /// </summary>
        /// <param name="columns">list of columns for creating Select List Item. For example, new SelectListItem { Value = x.Id.ToString(), Text = x.Name } </param>
        /// <param name="defaultText">Default text for Select List</param>
        /// <param name="defaultValue">Default value for Select List</param>
        /// <returns></returns>
        protected IEnumerable<SelectListItem> CreateSelectListItems(Expression<Func<TSource, SelectListItem>> columns, string defaultText = "-Select-", string defaultValue = "0")
        {
            IEnumerable<SelectListItem> items = Logic.GetSelectList(columns);
            if (!items.HasAny()) return null;
            var selectList = new List<SelectListItem> { new SelectListItem { Text = defaultText, Value = defaultValue } };
            selectList.AddRange(Logic.GetSelectList(columns));

            return selectList;
        }


        /// <summary>
        /// Add a new entity
        /// </summary>
        /// <param name="dto">Data Transfer Object for the entity</param>
        /// <returns></returns>
        [HttpPost("", Name = "Post[controller]")]
        [ProducesResponseType((int)HttpStatusCode.Created), ProducesResponseType((int)HttpStatusCode.InternalServerError), ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult Post([FromBody] TDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Logic.Post(dto);
                return Created();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }


        }
        /// <summary>
        /// Update an existing entity 
        /// </summary>
        /// <param name="updatedDto">Data Transfer Object for the entity</param>
        /// <returns></returns>
        [HttpPut("", Name = "Put[controller]")]
        [ProducesResponseType((int)HttpStatusCode.OK), ProducesResponseType((int)HttpStatusCode.InternalServerError), ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult Put([FromBody] TDto updatedDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Logic.Put(updatedDto.Id, updatedDto);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }


        }

        /// <summary>
        /// Removes an entity from the database
        /// </summary>
        /// <param name="id">Unique Identifier</param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = "Delete[controller]")]
        [ProducesResponseType((int)HttpStatusCode.NoContent), ProducesResponseType((int)HttpStatusCode.NotFound), ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult Delete(int id)
        {
            try
            {
                TSource entity = Logic.GetEntityById(id);
                if (entity == null)
                    return NotFound();
                Logic.Delete(entity);

                return NoContent();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }
    }
}