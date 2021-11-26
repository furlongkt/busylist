using System;
using BusyList.Utilities;
using Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CommonServiceLocator;
using Unity.ServiceLocation;
using BusyList.Views;

namespace BusyList
{
    public partial class App : Application
    {
        /// <summary>
        /// Dependency Injection container
        /// </summary>
        public readonly UnityContainer Container;
        public App()
        {
            InitializeComponent();

            //Create container and register types/instances
            Container = new UnityContainer();
            Container.RegisterAll();

            //Configure service locator to use the unity container
            var unityServiceLocator = new UnityServiceLocator(Container);
            ServiceLocator.SetLocatorProvider(()=> unityServiceLocator);

            //Instantiate the first Xamarin Page
            var navigationPage = new NavigationPage(new MainPage());
            MainPage = navigationPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
