using System;
using BusyList.Logging;
using BusyList.Managers;
using BusyList.Navigation;
using BusyList.Proxies;
using Unity;

namespace BusyList.Utilities
{
    /// <summary>
    /// This class is an extension class that makes it easier to register types with the dependency injection container.
    /// </summary>
    public static class ContainerRegistration
    {
        /// <summary>
        /// Register all types needed by the app into the dependency injection container
        /// </summary>
        /// <param name="container">Dependency Injection Container</param>
        public static void RegisterAll (this UnityContainer container)
        {
            container.RegisterType<ILogger, Logger>(TypeLifetime.Singleton);

            //Navigation Service
            container.RegisterType<INavigationService, NavigationService>(TypeLifetime.Singleton);

            //Proxies
            container.RegisterType<DbProxy>(TypeLifetime.Singleton);
            //TODO: Add web proxy here for increased persistence/interoperability between web/mobile.

            //Managers
            container.RegisterType<ListManager>(TypeLifetime.Singleton);
            container.RegisterType<ItemManager>(TypeLifetime.Singleton);
        }
    }
}
