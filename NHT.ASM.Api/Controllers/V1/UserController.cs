using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NHT.ASM.Bll.Interfaces;
using NHT.ASM.Helpers.ExtensionMethods;
using NHT.ASM.Infrastructure;
using NHT.ASM.Models.DataTransferObjects.UserModel;
using NHT.ASM.Models.Entities.UserModel;

namespace NHT.ASM.Api.Controllers.V1
{
    /// <summary>
    /// Controller for the <see cref="UserDto"/>
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [SuppressMessage("ReSharper", "InheritdocConsiderUsage")]
    public class UserController : CustomBaseController<User, UserDto>
    {
        private readonly  IUserLogic _userLogic;

        /// <summary>
        /// Instantiates new instance of controller
        /// </summary>
        /// <param name="userLogic">Logic class for this controller</param>
        /// <param name="context">DB context</param>
        /// <param name="hostingEnvironment">The current hosting environment</param>
        public UserController(IUserLogic userLogic, IAsmContext context, IHostingEnvironment hostingEnvironment) : base(userLogic, context, hostingEnvironment)
        {
            _userLogic = userLogic;
        }

        /// <summary>
        /// Gets the collection of <see cref="UserDto"/>
        /// </summary>
        /// <returns></returns>
        [HttpGet("", Name = "Get[controller]Users")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK),
         ProducesResponseType((int)HttpStatusCode.NotFound),
         ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult Get()
        {
            try
            {
                var userDtos = _userLogic.GetAll();
                if (userDtos.HasAny())
                {
                    return Ok(userDtos);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get a specific <see cref="UserDto" /> based on an unique identifier
        /// </summary>
        /// <param name="id">Unique identifier</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get[controller]ById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK), ProducesResponseType((int)HttpStatusCode.NotFound), ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult Get(int id)
        {
            try
            {
                UserDto userDto = _userLogic.GetById(id);
                if (userDto != null)
                {
                    return Ok(userDto);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }


    }
}
