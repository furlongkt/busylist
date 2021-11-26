using System;
namespace BusyList.Logging
{
    /// <summary>
    /// Interface to log data. Implementations can use a console log, file log,
    /// logging library or all of the above.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Create a debug log statement
        /// </summary>
        /// <param name="text">log message</param>
        void Debug(string text);
        /// <summary>
        /// Create a info log statement
        /// </summary>
        /// <param name="text">log message</param>
        void Info(string text);
        /// <summary>
        /// Create a warning log statement
        /// </summary>
        /// <param name="text">log message</param>
        void Warn(string text);
        /// <summary>
        /// Create an error log statement with stack trace from an exception
        /// </summary>
        /// <param name="e"></param>
        void Error(Exception e);
        /// <summary>
        /// Create an error log statement
        /// </summary>
        /// <param name="text">log message</param>
        void Error(string text);
    }
}
