using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusyList.Constants;
using BusyList.Models;
using BusyList.Extensions;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace BusyList.Proxies
{
    /// <summary>
    /// Proxy class that interacts with the local database.
    ///
    /// This class assumes all table entity models extend either
    /// <c>EntityBase</c>. 
    ///
    /// This class should not be used by viewmodels/ ui code behinds directly.
    /// All interaction with the database should be done behind a <c>Manager</c>
    /// </summary>
    /// <seealso cref="Managers"/>
    public class DbProxy : IProxy
    {
        //Use Lazy for delayed instantiation
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public DbProxy()
        {
            Type[] types = GetModelTypes();
            CreateTablesAsync(types).SafeFireAndForget(false);
        }

        /// <summary>
        /// Helper method to get all model types that need a table in the database
        /// </summary>
        /// <returns> an array of types that extend EntityBase</returns>
        private Type[] GetModelTypes()
        {
            var type = typeof(EntityBase);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));
            return types?.ToArray() ?? new Type[0];
        }

        /// <summary>
        /// Given an array of types, create their tables in the database.
        /// </summary>
        /// <param name="types">model types</param>
        /// <returns>awaitable task</returns>
        private async Task CreateTablesAsync(Type[] types)
        {
            if (types == null)
                throw new ArgumentNullException("types");

            if (!initialized)
            {
                //Create tables if they dont exist
                await Database.CreateTablesAsync(CreateFlags.None, types).ConfigureAwait(false);
                initialized = true;
            }
        }

        /// <summary>
        /// Get all rows in the table
        /// </summary>
        /// <typeparam name="T">model</typeparam>
        /// <returns></returns>
        public Task<List<T>> GetAllAsync<T>()
            where T : IEntity, new()
        {
            return Database.GetAllWithChildrenAsync<T>();
        }

        /// <summary>
        /// Get individual row corresponding to the provided id
        /// </summary>
        /// <typeparam name="T">model</typeparam>
        /// <param name="id">id of row</param>
        /// <returns>individual row</returns>
        public Task<T> GetAsync<T>(int id)
            where T : IEntity, new()
        {
            return Database.GetWithChildrenAsync<T>(id);
        }

        /// <summary>
        /// Save a row to the database.
        ///
        /// If the id does not exist yet, this method will create a new row.
        /// If the id already exists, this method will update the row completely
        /// with the new information.
        /// </summary>
        /// <typeparam name="T">model</typeparam>
        /// <param name="item">row</param>
        /// <returns></returns>
        public Task SaveAsync<T>(T item)
            where T : IEntity, new()
        {
            return Database.InsertOrReplaceWithChildrenAsync(item);
        }

        /// <summary>
        /// Removes a row from the database
        /// </summary>
        /// <typeparam name="T">model</typeparam>
        /// <param name="item">row</param>
        /// <returns>number of rows affected</returns>
        public Task<int> DeleteAsync<T>(T item)
            where T : IEntity, new()
        {
            return Database.DeleteAsync(item);
        }
    }
}
