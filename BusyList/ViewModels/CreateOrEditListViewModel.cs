using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusyList.Logging;
using BusyList.Managers;
using BusyList.Models;
using BusyList.Navigation;
using BusyList.Utilities;
using Xamarin.Forms;

namespace BusyList.ViewModels
{
    /// <summary>
    /// View model associated with <see cref="BusyList.Views.CreateOrEditList"/> page.
    /// </summary>
    public class CreateOrEditListViewModel : ViewModelBase
    {
        public readonly ILogger Logger;
        public readonly ListManager ListManager;
        public readonly INavigationService NavigationService;

        /// <summary>
        /// Color of indicator as a hex string
        /// </summary>
        public string ListColorHex
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        /// <summary>
        /// color of indicator as a color object
        /// </summary>
        /// <seealso cref="Color"/>
        public Color ListColor
        {
            get => GetValue<Color>();
            set
            {
                SetValue(value);
                ListColorHex = value.ToHex();
            }
        }

        /// <summary>
        /// Title of the list. This property fires OnPropertyChanged events when changed
        /// </summary>
        public string ListTitle
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        /// <summary>
        /// Subtitle of list. This property fires OnPropertyChanged events when changed
        /// </summary>
        public string ListSubtitle
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        /// <summary>
        /// Original list object. This property is populated when editing a list.
        /// </summary>
        public TodoList OriginalList {
            get=> GetValue<TodoList>();
            set
            {
                SetValue(value);
                if (!string.IsNullOrWhiteSpace(value?.Title))
                    ListTitle = value.Title;
                if (!string.IsNullOrWhiteSpace(value?.Subtitle))
                    ListSubtitle = value.Subtitle;
                if (!string.IsNullOrWhiteSpace(value?.Color))
                    SetColor(value.Color);
                else
                    SetColor(ColorUtil.RandomFlatColor());
            }
        }

        public CreateOrEditListViewModel(ILogger logger, ListManager listManager, INavigationService navigationService)
        {
            Logger = logger;
            ListManager = listManager;
            NavigationService = navigationService;
        }

        /// <summary>
        /// Set the list color
        /// </summary>
        /// <param name="color">Color to be set</param>
        public void SetColor(Color color)
        {
            ListColor = color;
            ListColorHex = color.ToHex();
        }

        /// <summary>
        /// Set the list color
        /// </summary>
        /// <param name="hex">color as hex string</param>
        public void SetColor(string hex)
        {
            ListColor = Color.FromHex(hex);
            ListColorHex = hex;
        }

        /// <summary>
        /// Initialize the viewmodel. Navigation parameter is expected to be a listId 
        /// </summary>
        /// <param name="navigationParameter">list id as int</param>
        /// <returns>awaitable task</returns>
        public override async Task InitializeAsync(object navigationParameter)
        {
            // If we are attaching a navigation parameter it means we are editing a list. otherwise we are creating a new one
            if(navigationParameter != null && navigationParameter is int listId && listId > 0)
            {
                OriginalList = await ListManager.GetList(listId);
            }
            else
            {
                //Pick a random color out of the flat color list 
                SetColor(ColorUtil.RandomFlatColor());
            }
        }

        /// <summary>
        /// Create/Update the list. If the list id does not yet exist it will
        /// create a new list, otherwise it will update the <c>OriginalList</c>
        /// object with the new values from the view and update the existing object
        /// in the persistent store
        /// </summary>
        /// <returns>awaitable task</returns>
        private async Task<bool> SaveList()
        {
            var list = OriginalList;
            //If original list is null create a new one with an empty id
            if (list == null)
            {
                list = new TodoList
                {
                    Items = new List<TodoItem>()
                };
            }
               
            list.Title = ListTitle;
            list.Subtitle = ListSubtitle;
            list.Color = ListColorHex;

            return await ListManager.SaveList(list);
        }

        /// <summary>
        /// Command to execute the save command and navigate back to the main screen.
        /// This command is auto registered because of the naming convention
        /// For more information on the code that handles the auto registration
        /// of this command please refer to <see cref="BusyList.ViewModels.ViewModelBase"/>
        /// </summary>
        /// <param name="state"></param>
        public async void Execute_Save(object state)
        {
            await SaveList();
            await NavigationService.NavigateToAsync<MainPageViewModel>();
        }

        /// <summary>
        /// CanExecute validation that corresponds to the save command.
        /// This can execute method is auto registered because of the naming convention
        /// For more information on the code that handles the auto registration
        /// of this command please refer to <see cref="BusyList.ViewModels.ViewModelBase"/>
        /// </summary>
        /// <param name="state"></param>
        public bool CanExecute_Save(object state)
        {
            return !string.IsNullOrWhiteSpace(ListTitle) &&
                   !string.IsNullOrWhiteSpace(ListSubtitle) &&
                   !string.IsNullOrWhiteSpace(ListColorHex);
        }
    }
}
