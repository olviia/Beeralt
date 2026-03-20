using System;
using UnityEngine;

namespace UI
{
    public abstract class PausingMenu: MonoBehaviour, IMenu
    {
        public virtual void Open()
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }

        //template method pattern
        // i give the children this method so they don't implement the interface
        // they just call a method and the event is invoked from their parent - this class
        //protected virtual raiser pattern?
        protected void RequestClose() => OnCloseRequested?.Invoke(this);
        protected void RequestOpen() => OnOpenRequested?.Invoke(this);
        public event Action<IMenu> OnCloseRequested;
        public event Action<IMenu> OnOpenRequested;
    }
}