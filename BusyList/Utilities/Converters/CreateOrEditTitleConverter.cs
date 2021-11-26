using System;
using System.Globalization;
using BusyList.Models;
using Xamarin.Forms;

namespace BusyList.Utilities.Converters
{
    /// <summary>
    /// Utility to one-way convert a TodoList instance into a create or edit title.
    /// </summary>
    public class CreateOrEditTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is TodoList list && !string.IsNullOrWhiteSpace(list?.Title))
            {
                return "Edit List";
            }
            return "Create New List";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
