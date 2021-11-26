using System;
using System.Threading.Tasks;
using BusyList.ViewModels;

namespace BusyList.Navigation
{
    /// <summary>
    /// Interface for Injectable access to View-model first navigation.
    /// </summary>
    /// <seealso cref="NavigationService"/>
    /// <seealso cref="NavigationPage"/>
    public interface INavigationService
    {
        /// <summary>
        /// Previous page's view model.
        /// </summary>
        ViewModelBase PreviousPageViewModel { get; }
        /// <summary>
        /// Navigate to the page associated with <c>TViewModel</c>. Whenever a
        /// navigation occurs via this method, the destination viewmodel's
        /// <c>InitializeAsync</c> method is called.
        ///
        /// The namespace and naming structure is very important.
        /// <example>
        /// For a new page called <c>MyPage</c>. You'll need to create a new
        /// <c>ContentPage</c> and its code behind called <c>MyPage.xaml</c>
        /// and <c>MyPage.xaml.cs</c> located in the <c>BusyList.Views</c> namespace
        ///
        /// You'll also need the viewmodel to be called <c>MyPageViewModel</c>
        /// and place it in the <c>BusyList.ViewModels</c> namespace
        /// </example>
        /// </summary>
        /// <typeparam name="TViewModel">View model type</typeparam>
        /// <returns>awaitable task</returns>
        /// <seealso cref="ViewModelBase.InitializeAsync(object)"/>
        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;
        /// <summary>
        /// Navigate to the page associated with <c>TViewModel</c> while passing
        /// a parameter to the new view controller's <c>InitializeAsync</c> method.
        ///
        /// The namespace and naming structure is very important.
        /// <example>
        /// For a new page called <c>MyPage</c>. You'll need to create a new
        /// <c>ContentPage</c> and its code behind called <c>MyPage.xaml</c>
        /// and <c>MyPage.xaml.cs</c> located in the <c>BusyList.Views</c> namespace
        ///
        /// You'll also need the viewmodel to be called <c>MyPageViewModel</c>
        /// and place it in the <c>BusyList.ViewModels</c> namespace
        /// </example>
        /// </summary>
        /// <typeparam name="TViewModel">View model type</typeparam>
        /// <returns>awaitable task</returns>
        /// <seealso cref="ViewModelBase.InitializeAsync(object)"/>
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        /// <summary>
        /// Remove the most recent page from the back stack. This method does
        /// not perform any navigation and only handles the backstack
        /// </summary>
        /// <returns>awaitable task</returns>
        Task RemoveLastFromBackStackAsync();
        /// <summary>
        /// Removes all pages from the back stack. This method does
        /// not perform any navigation and only handles the backstack
        /// </summary>
        /// <returns>awaitable task</returns>
        Task ClearBackStackAsync();
    }
}
