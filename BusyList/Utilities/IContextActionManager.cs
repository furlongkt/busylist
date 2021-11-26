using System;
namespace BusyList.Utilities
{
    /// <summary>
    /// Interface used to natively close the context menu (context actions on a listview)
    /// </summary>
    public interface IContextActionManager
    {
        void CloseLastContextMenu();
    }
}
