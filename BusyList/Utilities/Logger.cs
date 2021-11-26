using System;
namespace BusyList.Logging
{
    /// <summary>
    /// Basic logging implementation that writes to console
    /// </summary>
    public class Logger : ILogger
    {
        /// <summary>
        /// Creates a debug log statement
        /// </summary>
        /// <param name="text">log message</param>
        public void Debug(string text)
        {
            Log(LogType.DEBUG, text);
        }

        /// <summary>
        /// Creates an error log statement with a stack trace from an exception
        /// </summary>
        /// <param name="text">log message</param>
        public void Error(Exception e)
        {
            Log(LogType.ERROR, "", e);
        }

        /// <summary>
        /// Creates an error log statement
        /// </summary>
        /// <param name="text">log message</param>
        public void Error(string text)
        {
            Log(LogType.ERROR, text);
        }

        /// <summary>
        /// Creates a info log statement
        /// </summary>
        /// <param name="text">log message</param>
        public void Info(string text)
        {
            Log(LogType.INFO, text);
        }

        /// <summary>
        /// Creates a warning log statement
        /// </summary>
        /// <param name="text">log message</param>
        public void Warn(string text)
        {
            Log(LogType.WARNING, text);
        }

        /// <summary>
        /// Creates a log statement. If the message is empty, the exception message will be used
        /// </summary>
        /// <param name="type"> type of statement</param>
        /// <param name="message">log message (optional) </param>
        /// <param name="e">exception to log (optional)</param>
        private void Log(LogType type, string message = null, Exception e = null)
        {
            //Can easily be replaced by logging library and/or file log.
            Console.WriteLine($"[{DateTime.Now}] | {type} | {(string.IsNullOrEmpty(message) ? (e?.Message ?? "") : message)}");
            if(e!=null)
                Console.WriteLine(e.ToString());

        } 

        /// <summary>
        /// Enum indicating log type
        /// </summary>
        private enum LogType
        {
            DEBUG,
            INFO,
            WARNING,
            ERROR
        }
    }
}
