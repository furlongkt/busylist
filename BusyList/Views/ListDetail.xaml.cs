using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusyList.Models;
using BusyList.ViewModels;
using CommonServiceLocator;
using Xamarin.Forms;

namespace BusyList.Views
{
    public partial class ListDetail : ContentPage
    {
        public ListDetailViewModel ViewModel { get; }

        public ListDetail()
        {
            InitializeComponent();
            ViewModel = ServiceLocator.Current.GetInstance<ListDetailViewModel>();
            BindingContext = ViewModel;
        }

        /// <summary>
        /// Handle editing an task
        /// </summary>
        /// <param name="sender">edit context action menu item</param>
        /// <param name="e"></param>
        public async void OnEdit(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var item = mi.CommandParameter as TodoItem;

            await ShowEditTaskPrompt(item);
        }

        /// <summary>
        /// Handle deleting a task. This method will show a confirmation dialog
        /// to confirm deletion before performing the destructive action
        /// </summary>
        /// <param name="sender">delete context action menu item</param>
        /// <param name="e"></param>
        public async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var item = mi.CommandParameter as TodoItem;

            var result = await DisplayAlert($"Delete {item.Name}?", $"Do you really want to delete {item.Name}?", "Yes", "No");
            if (result)
                await ViewModel.DeleteTask(item);
        }

        /// <summary>
        /// Handle create task
        /// </summary>
        /// <param name="sender">n/a</param>
        /// <param name="e">n/a</param>
        async void btnCreateTask_Clicked(System.Object sender, System.EventArgs e)
        {
            var result = await DisplayPromptAsync("Create new task", "Describe your task.");

            if (!string.IsNullOrWhiteSpace(result))
                await ViewModel.CreateNewTask(result);
        }

        /// <summary>
        /// Handle toggling a tasks <c>Is Completed</c> checkbox
        /// </summary>
        /// <param name="sender">Checkbox</param>
        /// <param name="e"></param>
        async void ckCompleted_CheckedChanged(System.Object sender, CheckedChangedEventArgs e)
        {
            var item = (sender as CheckBox)?.BindingContext as TodoItem;
            if(item != null)
                await ViewModel.UpdateTask(item);
        }

        /// <summary>
        /// Handle Tapping on a task in the task list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void lvTasks_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var item = e?.Item as TodoItem;
            await ShowEditTaskPrompt(item);
        }

        /// <summary>
        /// Show the user a UI prompt to change the text of a task
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private async Task ShowEditTaskPrompt(TodoItem item)
        {
            var result = await DisplayPromptAsync("Update task", "Describe your task.", initialValue: item.Name);
            if (!string.IsNullOrWhiteSpace(result))
            {
                item.Name = result;
                await ViewModel.UpdateTask(item, true);
            }
        }

    }
}
