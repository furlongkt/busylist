using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using BusyList.Logging;
using BusyList.Managers;
using BusyList.Models;
using Xamarin.Forms;

namespace BusyList.ViewModels
{
    /// <summary>
    /// View model associated with <see cref="BusyList.Views.ListDetail"/> page.
    /// </summary>
    public class ListDetailViewModel : ViewModelBase
    {
        public readonly ILogger Logger;
        public readonly ListManager ListManager;
        public readonly ItemManager ItemManager;

        /// <summary>
        /// TodoList object that is currently showing details for.
        /// </summary>
        public TodoList SelectedList { get; set; }

        /// <summary>
        /// Observable name of the list
        /// </summary>
        public string ListName
        {
            get=>GetValue<string>();
            set=>SetValue(value);
        }

        /// <summary>
        /// Observable list of tasks included in this list.
        /// </summary>
        public ObservableCollection<TodoItem> Items
        {
            get => GetValue<ObservableCollection<TodoItem>>();
            set => SetValue(value);
        }

        public ListDetailViewModel(ILogger logger, ListManager listManager, ItemManager itemManager)
        {
            Logger = logger;
            ListManager = listManager;
            ItemManager = itemManager;

            Items = new ObservableCollection<TodoItem>();

        }

        /// <summary>
        /// Initializes the view model. Navigation parameter is expected to be the list ID as an integer
        /// </summary>
        /// <param name="navigationParameter">list id as int</param>
        /// <returns></returns>
        public override async Task InitializeAsync(object navigationParameter)
        {
            if (navigationParameter != null && navigationParameter is int listId && listId > 0)
            {
                await RefreshData(listId);
            }
        }

        /// <summary>
        /// Refreshes view model data with data from the list manager
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        private async Task RefreshData(int listId)
        {
            SelectedList = await ListManager.GetList(listId);
            Items = new ObservableCollection<TodoItem>(SelectedList.Items);
            ListName = SelectedList.Title;
        }

        /// <summary>
        /// Update an individual task/todo item. 
        /// </summary>
        /// <param name="item">task to be updated</param>
        /// <param name="UpdateListItemInMemory">flag indicating whether or not to also fire an update to the object in memory</param>
        /// <returns></returns>
        internal async Task UpdateTask(TodoItem item, bool UpdateListItemInMemory = false)
        {
            if (UpdateListItemInMemory)
            {
                var index = Items.IndexOf(item);
                if (index >= 0)
                    Items[index] = item;
            }
            await ItemManager.SaveItem(item);
        }

        /// <summary>
        /// Delete an individual task
        /// </summary>
        /// <param name="item">task to be deleted</param>
        /// <returns></returns>
        internal async Task DeleteTask(TodoItem item)
        {
            await ItemManager.DeleteItem(item);
            await RefreshData(SelectedList.Id ?? 0);
        }


        /// <summary>
        /// Create new task marked as incomplete and associate it with the current list
        /// </summary>
        /// <param name="task">task title</param>
        /// <returns>awaitable task</returns>
        internal async Task CreateNewTask(string task)
        {
            var item = new TodoItem { Name = task, IsCompleted = false, ListId = SelectedList.Id ?? 0 };            
            await ItemManager.SaveItem(item);
            await RefreshData(SelectedList.Id ?? 0);
        }
    }
}
