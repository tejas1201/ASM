using System;
using System.Collections.Generic;

namespace NHT.ASM.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResults
    {
        /// <summary>
        /// Errors that have occured
        /// </summary>
        List<Exception> Exceptions { get; set; }
        /// <summary>
        /// Where the results retrieved successfully
        /// </summary>
        bool Succes { get; set; }

        /// <summary>
        /// Reset the status of the result object
        /// </summary>
        void Reset();
        
        /// <summary>
        /// Number of errors that have occured
        /// </summary>
        int ErrorCount { get; set; }
    }
}