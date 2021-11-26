using Xamarin.Forms;

namespace BusyList.Utilities.Validation
{
    public interface IErrorStyle
    {
        void ShowError(View view, string message);
        void RemoveError(View view);
    }
}
