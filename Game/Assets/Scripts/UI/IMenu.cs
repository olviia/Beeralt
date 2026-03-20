using System;

namespace UI
{
    public interface IMenu
    {
        public void Open();
        public void Close();
        public event Action<IMenu> OnCloseRequested;
        public event Action<IMenu> OnOpenRequested;
    }
}