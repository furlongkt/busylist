using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using BusyList.ViewModels;
using BusyList.Views;
using Xamarin.Forms;

namespace BusyList.Navigation
{
    /// <summary>
    /// Implementation of <c>INavigationService</c>. This is an injectable
    /// service that enables view models to perform navigation
    /// </summary>
    public class NavigationService : INavigationService
    {
        public ViewModelBase PreviousPageViewModel
        {
            get
            {
                var mainPage = Application.Current.MainPage as NavigationPage;
                var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as ViewModelBase;
            }
        }

        /// <summary>
        /// Required default constructor
        /// </summary>
        public NavigationService()
        {
        }


        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as NavigationPage;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public Task ClearBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as NavigationPage;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// Navigate to a page asyncronously
        /// </summary>
        /// <param name="viewModelType">Type of view model associated with the page</param>
        /// <param name="parameter">Parameters to pass to view model's <c>InitializeAsync</c> method</param>
        /// <returns>awaitable task</returns>
        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, parameter);

            if (page is MainPage)
            {
                Application.Current.MainPage = new NavigationPage(page);
            }
            else
            {
                var navigationPage = Application.Current.MainPage as NavigationPage;
                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    Application.Current.MainPage = new NavigationPage(page);
                }
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        /// <summary>
        /// Given a viewmodel type return the page type that is associated with it.
        /// File names, plurality, and namespaces are very important.
        ///
        /// If the page name is MyPage. The following files should be created.:
        /// <list type="number">
        /// <item><c>BusyList.Views.MyPage.xaml</c> - UI xml file</item>
        /// <item><c>BusyList.Views.MyPage.xaml.cs</c> - UI code behind </item>
        /// <item><c>BusyList.ViewModels.MyPageViewModel.cs</c> - View model</item>
        /// </summary>
        /// <param name="viewModelType">type of viewmodel</param>
        /// <returns>type of page associated with view model</returns>
        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Models", "s");
            viewName = viewName.Replace("ViewModel", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }
    }
}
