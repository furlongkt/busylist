using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusyList.Logging;
using BusyList.Models;
using BusyList.Proxies;

namespace BusyList.Managers
{
    /// <summary>
    /// Class to manage To-do list items
    /// </summary>
    public class ItemManager : BaseManager
    {
        public ItemManager(ILogger logger, DbProxy dbProxy) : base(logger, dbProxy)
        {
        }

        /// <summary>
        /// Save a <c>TodoItem</c>. If the <c>item</c> does not yet exist,
        /// one will be created. If it does already exist, it will be updated.
        /// </summary>
        /// <param name="item">item to save</param>
        /// <returns>awaitable task</returns>
        internal async Task SaveItem(TodoItem item)
        {
            await DbProxy.SaveAsync(item);
        }

        /// <summary>
        /// Deletes a <c>TodoItem</c>. This is a full delete NOT a soft delete
        /// </summary>
        /// <param name="item">item to be deleted</param>
        /// <returns>awaitable Task</returns>
        internal async Task DeleteItem(TodoItem item)
        {
            await DbProxy.DeleteAsync(item);
        }

    }

}
