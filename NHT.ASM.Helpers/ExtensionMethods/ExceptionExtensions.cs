
using System;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace NHT.ASM.Helpers.ExtensionMethods
{
    /// <summary>
    /// Extensions for exeption handling
    /// </summary>
    public static class ExceptionExtensions
    {
        #region private fields

        private static readonly Logger Logger = LogManager.GetLogger("Logger");

        #endregion

        #region private methods

        /// <summary>
        /// Method used to convert exception's innerMessage to string
        /// </summary>
        /// <param name="e">exception</param>
        /// <param name="message">message string</param>
        /// <returns>modified message</returns>
        private static string InnerMessagesToString(this Exception e, string message)
        {
            if (string.IsNullOrEmpty(e.InnerException?.Message)) return message;

            message += $"Innermessage: {e.InnerException.Message}, ";
            message = InnerMessagesToString(e.InnerException, message);
            return message;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Creates a list of strings for a list of exceptions to return to the client
        /// </summary>
        /// <param name="el">list of exception</param>
        /// <returns>error message list</returns>
        public static List<string> ToList(this List<Exception> el)
        {
            var messages = new List<string>();
            foreach (var e in el) messages.Add(e.ToCustomString());
            return messages;
        }

        /// <summary>
        /// Creates and returns a custom (message, stacktrace and innermessages) string representation of the current exception.
        /// </summary>
        /// <param name="e">exception</param>
        /// <returns>A string representation of the current exception.</returns>
        public static string ToCustomString(this Exception e)
        {
            string message = "";
            if (e.InnerException == null)
            {
                message = $"Message: {e.Message}";
            }
            else
            {
                message = InnerMessagesToString(e, message);
                message += $"Stacktrace: {e.StackTrace ?? string.Empty}";
            }

            Logger.Error(e);

            return message;
        }

        /// <summary>
        /// Creates a  strings out of a list of exceptions to return to the client
        /// </summary>
        /// <param name="el">exception</param>
        /// <returns>one exception string</returns>
        public static string ToOneExceptionString(this List<Exception> el)
        {
            var fullMessage = el.ToList().Aggregate("", (current, errorString) => current + errorString + @"\n");
            return fullMessage;
        }

        #endregion
    }
}