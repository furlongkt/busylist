using System;
using BusyList.Logging;
using BusyList.Proxies;

namespace BusyList.Managers
{
    /// <summary>
    /// A Manager is essentially the coordinator between different data stores(<c>Proxies</c>).
    /// It allows the ViewModels and UI codebehind to have one interface to call
    /// to get data while still reaping the benefits of multiple data stores
    /// like fall back to a local store when offline, offline queuing,
    /// microservice logic, caching..etc
    ///
    /// To be clear, the UI and ViewModels should never interact with the Proxies
    /// directly, they should always use a manager.
    ///
    /// This class is the base class for a manager.
    ///
    /// <example>
    /// Implementenation: 
    /// <code>
    /// public class ItemManager : BaseManager
    /// {
    ///     public ItemManager(ILogger logger, DbProxy dbProxy) : base(logger, dbProxy)
    ///     {
    ///     }
    ///
    ///     internal async Task GetItem(int? id)
    ///     {
    ///         /* TODO: Get item from web and update local database
    ///                  On success the database will contain the latest value
    ///                  On failure do nothing except maybe log the error.
    ///                  By doing nothing on failure we are allowing the use of the
    ///                  local db value as a cached value
    ///         */
    ///         if((id ?? 0) > 0)
    ///             return await DbProxy.GetAsync<MyItem>(id);
    ///         else
    ///             return null;
    ///     }
    /// 
    ///     internal async Task SaveItem(Item item)
    ///     {
    ///         await DbProxy.SaveAsync(item);
    ///         //TODOAdd Web API update here
    ///     }
    ///     internal async Task DeleteItem(Item item)
    ///     {
    ///         await DbProxy.DeleteAsync(item);
    ///         //TODO: Add web api update here
    ///     }
    /// }
    /// </code>
    ///
    /// Register the proxy(if applicable) and the manager with the dependency injection container:
    /// File: <c>BusyList.Extensions.ContainerRegistration.cs</c>
    /// <code>
    /// public static void RegisterAll (this UnityContainer container)
    /// {
    ///    ...
    ///    //Proxies
    ///    ...
    ///    container.RegisterType<MyProxy>(TypeLifetime.Singleton);
    ///    ...
    ///    
    ///    //Managers
    ///    ...
    ///    container.RegisterType<ItemManager>(TypeLifetime.Singleton);
    ///    ...
    /// }
    /// </code>
    /// </example>
    /// </summary>
    /// <seealso cref="Proxies"/>
    /// <seealso cref="Extensions.ContainerRegistration"/>
    public abstract class BaseManager
    {
        /// <summary>
        /// Reference to the logger
        /// </summary>
        protected readonly ILogger Logger;
        /// <summary>
        /// Reference to the database proxy
        /// </summary>
        protected readonly DbProxy DbProxy;

        public BaseManager(ILogger logger, DbProxy dbProxy)
        {
            Logger = logger;
            DbProxy = dbProxy;
        }
    }
}
