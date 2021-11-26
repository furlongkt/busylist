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
    /// Class to manage todo lists
    /// </summary>
    public class ListManager : BaseManager
    {
        public ListManager(ILogger logger, DbProxy dbProxy) : base(logger, dbProxy)
        {
        }

        /// <summary>
        /// Get all todo lists
        /// </summary>
        /// <returns>List of TodoLists</returns>
        public Task<List<TodoList>> GetLists()
        {
            return DbProxy.GetAllAsync<TodoList>();
        }

        /// <summary>
        /// Given an id, get an individual <c>TodoList</c>. If the id does not
        /// correspond to a todo list this method will return <c>null</c>.
        /// </summary>
        /// <param name="id">list id</param>
        /// <returns>TodoList instance or null</returns>
        internal async Task<TodoList> GetList(int? id)
        {
            if (id > 0)
                return await DbProxy.GetAsync<TodoList>(id ?? 0);
            else
                return null;
        }

        /// <summary>
        /// Saves a TodoList.
        /// If the list does not yet exist this method acts as an insert
        /// If the list already exists this method acts as an upsert
        /// </summary>
        /// <param name="list">list to be saved</param>
        /// <returns>awaitable Task</returns>
        public async Task<bool> SaveList(TodoList list)
        {
            await DbProxy.SaveAsync(list);
            //TODO: replace with confirmation of success/failure
            return true;
        }

        /// <summary>
        /// Delete a <c>TodoList</c>
        /// This is a full delete NOT a soft delete
        /// </summary>
        /// <param name="list">list to be deleted</param>
        /// <returns>boolean indicating success/failure</returns>
        internal async Task<bool> DeleteList(TodoList list)
        {
            int numAffected = await DbProxy.DeleteAsync(list);
            return numAffected > 0;
        }
    }
}
