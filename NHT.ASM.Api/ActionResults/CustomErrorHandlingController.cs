using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NHT.ASM.Common.Resources;

namespace NHT.ASM.Api.ActionResults
{
    /// <summary>
    /// Extends default ApiController error handling
    /// </summary>
    [ApiController]
    [SuppressMessage("ReSharper", "InheritdocConsiderUsage")]
    public class CustomErrorHandlingController : ControllerBase
    {

        /// <summary>
        /// The current hosting environment
        /// </summary>
        protected internal readonly IHostingEnvironment HostingEnvironment;

        /// <summary>
        /// Instantiates new instance and sets the <see cref="HostingEnvironment"/>
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        protected internal CustomErrorHandlingController(IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Creates a 400 Bad Request response with the specified error message.
        /// </summary>
        /// <param name="message">The user-visible error message.</param>
        /// <returns>A <see cref="CustomActionResult"/> with the error message</returns>
        protected internal virtual CustomActionResult BadRequest(string message)
        {
            return new CustomActionResult(HttpStatusCode.BadRequest, message);
        }

        /// <summary>
        /// Creates a 400 Bad Request response with the specified error messages.
        /// </summary>
        /// <param name="messages">The user-visible error messages.</param>
        /// <returns>A <see cref="CustomActionResult"/> with the error message.</returns>
        protected internal virtual CustomActionResult BadRequest(List<string> messages)
        {
            return new CustomActionResult(HttpStatusCode.BadRequest, messages.ToString());
        }

        /// <summary>
        /// Creates a 500 Internal server error response with a user friendly error messages.
        /// </summary>
        /// <param name="exception">The error</param>
        /// <returns>A <see cref="CustomActionResult"/> with the error message.</returns>
        protected internal virtual CustomActionResult InternalServerError(Exception exception)
        {
            return new CustomActionResult(HttpStatusCode.InternalServerError, Messages.SomethingWentWrong, exception, HostingEnvironment);
        }

        /// <summary>
        /// Creates a 440 Not Found resonse with a user friendly error message
        /// </summary>
        /// <param name="message">The user-visible error message.</param>
        /// <returns>A <see cref="CustomActionResult"/> with the error message.</returns>
        protected internal virtual CustomActionResult NotFound(string message)
        {
            return new CustomActionResult(HttpStatusCode.NotFound, message);
        }

        /// <summary>
        /// Creates a 201 Created resonse with a user friendly success message
        /// </summary>
        /// <param name="message">The user-visible success message.</param>
        /// <returns>A <see cref="CustomActionResult"/> with the success message.</returns>
        protected internal virtual CustomActionResult Created(string message = null)
        {
            if (string.IsNullOrEmpty(message))
                message = Messages.CreatedSuccessfully;
            return new CustomActionResult(HttpStatusCode.Created, message);
        }

        
        /// <summary>
        /// Creates a 204 NoContent resonse with a user friendly error message
        /// </summary>
        /// <param name="message">The user-visible NoContent message.</param>
        /// <returns>A <see cref="CustomActionResult"/> with the  message.</returns>
        protected internal virtual CustomActionResult NoContent(string message)
        {
            return new CustomActionResult(HttpStatusCode.NoContent, message);
        }

        /// <summary>
        /// Creates a 403 Forbidden resonse with a user friendly error message
        /// </summary>
        /// <param name="message">The user-visible forbidden message.</param>
        /// <returns>A <see cref="CustomActionResult"/> with the  message.</returns>
        protected internal virtual CustomActionResult Forbidden(string message)
        {
            return new CustomActionResult(HttpStatusCode.Forbidden, message);
        }
    }
}
