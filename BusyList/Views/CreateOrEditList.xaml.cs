using System;
using System.Collections.Generic;
using BusyList.Logging;
using BusyList.Managers;
using BusyList.Models;
using BusyList.ViewModels;
using CommonServiceLocator;
using Xamarin.Forms;

namespace BusyList.Views
{
    public partial class CreateOrEditList : ContentPage
    {
        private readonly CreateOrEditListViewModel ViewModel;

        public CreateOrEditList()
        {
            InitializeComponent();
            ViewModel = ServiceLocator.Current.GetInstance<CreateOrEditListViewModel>();
            BindingContext = ViewModel;
        }
    }
}
