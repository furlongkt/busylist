using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BusyList.Logging;
using BusyList.Managers;
using BusyList.Models;
using BusyList.Navigation;
using Xamarin.Forms;

namespace BusyList.ViewModels
{
    /// <summary>
    /// View model associated with <see cref="BusyList.Views.MainPage"/> page.
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {
        public ILogger Logger;
        public ListManager ListManager;
        public INavigationService NavigationService;

        /// <summary>
        /// Observable collection of lists
        /// </summary>
        public ObservableCollection<TodoList> Lists
        {
            get => GetValue<ObservableCollection<TodoList>>();
            set => SetValue(value);
        }

        public MainPageViewModel(ILogger logger, ListManager listManager, INavigationService navigationService)
        {
            Logger = logger;
            ListManager = listManager;
            NavigationService = navigationService;
            Lists = new ObservableCollection<TodoList>();
        }

        /// <summary>
        /// Initialize view model. No navigation parameters are expected. 
        /// </summary>
        /// <param name="navigationParameter">n/a</param>
        /// <returns>awaitable task</returns>
        public override async Task InitializeAsync(object navigationParameter)
        {
            await RefreshData();
        }

        /// <summary>
        /// Refresh data in the view model with data from the list manager
        /// </summary>
        /// <returns></returns>
        public async Task RefreshData()
        {
            Lists = new ObservableCollection<TodoList>(await ListManager.GetLists());
        }

        /// <summary>
        /// delete an individual list
        /// </summary>
        /// <param name="list">list to be deleted</param>
        /// <returns>boolean indicating success/failure</returns>
        internal async Task<bool> DeleteList(TodoList list)
        {
           var success = await ListManager.DeleteList(list);
            if (success)
                Lists.Remove(list);
            return success;
        }

        /// <summary>
        /// Navigate to list details page, do nothing if <c>listId <= 0</c>
        /// </summary>
        /// <param name="listId">list id </param>
        public async void GoToListDetail(int listId)
        {
            if(listId > 0)
                await NavigationService.NavigateToAsync<ListDetailViewModel>(listId);
        }

        /// <summary>
        /// Navigate to the create or edit list screen.
        ///
        /// If the list ID is populated, it will navigate to the screen to edit a list
        /// Otherwise it will navigate to the screen to create a new list
        /// </summary>
        /// <param name="listId">list ID, defaults to 0 to indicate list creation</param>
        /// <returns>awaitable task</returns>
        public Task GoToListCreateOrEdit(int listId = 0)
        {
            return NavigationService.NavigateToAsync<CreateOrEditListViewModel>(listId);
        }


        /// <summary>
        /// Command to navigate to the create a new list screen.
        ///
        /// This command is auto registered because of the naming convention
        /// For more information on the code that handles the auto registration
        /// of this command please refer to <see cref="BusyList.ViewModels.ViewModelBase"/>
        /// </summary>
        /// <param name="state">n/a</param>
        public async void Execute_CreateList(object state)
        {
            await GoToListCreateOrEdit();
        }
    }
}
