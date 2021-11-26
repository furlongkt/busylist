using System;
using System.Globalization;
using Xamarin.Forms;

namespace BusyList.Utilities.Converters
{
    /// <summary>
    /// Utility to one-way convert a boolean into a text decoration. The
    /// boolean represents whether or not a task is completed and the text decoration is a strikethrough (true) or None (false)
    /// </summary>
    public class IsCompletedToTextDecoration : IValueConverter
    {
        public object Convert(object value, Type targetType, object para, CultureInfo culture)
        {
            if (value is bool isCompleted && isCompleted)
                return TextDecorations.Strikethrough;
            else return TextDecorations.None;
        }
        public object ConvertBack(object value, Type targetType, object para, CultureInfo culture)
        {
            return new NotImplementedException();
        }
    }
}
