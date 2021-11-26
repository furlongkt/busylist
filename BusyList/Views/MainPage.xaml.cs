using System;
using BusyList.Models;
using BusyList.Utilities;
using BusyList.ViewModels;
using CommonServiceLocator;
using Xamarin.Forms;

namespace BusyList.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel ViewModel;
        public MainPage()
        {
            InitializeComponent();

            ViewModel = ServiceLocator.Current.GetInstance<MainPageViewModel>();
            BindingContext = ViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //Refresh the view model whenever we reappear to get fresh data
            await ViewModel.InitializeAsync(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //If we trigger a context action by pressing and holding on a list
            //then we try to navigate to create a new list, the context action bar will remain on the screen
            //So this code removes the last displayed context action bar.
            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<IContextActionManager>().CloseLastContextMenu();
        }

        /// <summary>
        /// Handle editing a list
        /// </summary>
        /// <param name="sender">edit context action menu item</param>
        /// <param name="e"></param>
        public async void OnEdit(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var list = mi.CommandParameter as TodoList;

            await ViewModel.GoToListCreateOrEdit(list.Id ?? 0);
        }

        /// <summary>
        /// Handle deleting a list by showing a confirmation dialog before performing the destructive action
        /// </summary>
        /// <param name="sender">delete context action menu item</param>
        /// <param name="e"></param>
        public async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var list = mi.CommandParameter as TodoList;

            var result = await DisplayAlert($"Delete {list.Title}?", $"Do you really want to delete {list.Title}?", "Yes", "No");
            if (result)
                await ViewModel.DeleteList(list);
        }

        /// <summary>
        /// Handle tapping on a list in the list of lists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lvLists_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var item = e?.Item as TodoList;
            ViewModel.GoToListDetail(item?.Id ?? 0);
        }
    }
}
