using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BusyList.Utilities
{
    /// <summary>
    /// This class contains the basics of a property that can be observed
    /// </summary>
    public class ObservableProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
