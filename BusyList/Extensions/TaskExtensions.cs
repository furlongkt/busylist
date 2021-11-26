using System;
using System.Threading.Tasks;

namespace BusyList.Extensions
{
    public static class TaskExtensions
    {
        // NOTE: Async void is intentional here. This provides a way
        // to call an async method from the constructor while
        // communicating intent to fire and forget, and allow
        // handling of exceptions
        /// <summary>
        /// Task extension to safely run the task with/without the need to go back to the calling context. This allows one to fire the task and forget about the return/response: "Fire and forget"
        /// </summary>
        /// <param name="task">Task</param>
        /// <param name="returnToCallingContext">boolean indicating whether or not to await task and return or allow the caller to continue without waiting</param>
        /// <param name="onException">Exception handler</param>
        public static async void SafeFireAndForget(this Task task,
            bool returnToCallingContext,
            Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(returnToCallingContext);
            }

            // if the provided action is not null, catch and
            // pass the thrown exception
            catch (Exception ex) when (onException != null)
            {
                onException(ex);
            }
        }
    }
}
