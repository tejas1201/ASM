using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NHT.ASM.Common.Resources;

namespace NHT.ASM.Api.ActionResults
{
    /// <summary>
    /// Base class for custom action results to override things like status code or error message.
    /// </summary>
    /// <remarks>Taken from: https://gist.github.com/jadhavajay/7638562.</remarks>
    // ReSharper disable once InheritdocConsiderUsage
    public class CustomActionResult : ActionResult
    {
        /// <summary>
        /// The user friendly error message
        /// </summary>
        public string Message;

        /// <summary>
        /// Exception that has been thrown
        /// </summary>
        public readonly Exception Exception;
        
        private readonly HttpStatusCode _statusCode;


        /// <inheritdoc />
        public CustomActionResult(HttpStatusCode statusCode, string errorMessage)
            : this(statusCode, errorMessage, null, null)
        {
            _statusCode = statusCode;
            Message = errorMessage;
        }

        /// <summary>
        /// Creates a new instance of the CustomActionResult class.
        /// </summary>
        /// <param name="statusCode">The custom HTTP status code.</param>
        /// <param name="errorMessage">The error message added to the body of the result.</param>
        /// <param name="exception">The exception that is added to the body of the result,</param>
        /// <param name="hostingEnvironment">The hostingenvironment to check whether to send the exception message to the client</param>
        public CustomActionResult(HttpStatusCode statusCode, string errorMessage, Exception exception, IHostingEnvironment hostingEnvironment)
        {
            _statusCode = statusCode;
            if (exception != null && hostingEnvironment != null && hostingEnvironment.IsDevelopment())
            {
                Exception = exception;
            }
            Message = string.IsNullOrEmpty(errorMessage) ? Messages.SomethingWentWrong : errorMessage;
        }

        /// <inheritdoc />
        public override async Task ExecuteResultAsync(ActionContext context)
        {
            var jsonResult = new JsonResult(this)
            {
                StatusCode = (int) _statusCode
            };
            await jsonResult.ExecuteResultAsync(context);
        }


        /// <inheritdoc />
        public override void ExecuteResult(ActionContext context)
        {
            var jsonResult = new JsonResult(this)
            {
                StatusCode = (int)_statusCode
            };
            jsonResult.ExecuteResult(context);
        }
    }
}
