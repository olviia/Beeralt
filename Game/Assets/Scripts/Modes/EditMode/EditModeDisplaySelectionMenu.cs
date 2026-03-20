using System;
using Content.ObjectsThatExistOnScene;
using TMPro;
using UI;
using UnityEngine;

namespace Modes.EditMode
{
    public class EditModeDisplaySelectionMenu:MonoBehaviour, IMenu
    {
        private IContentNavigator contentNavigator;
        
        //here will go my elements left to right
        [SerializeField] private TMP_Text[] slots;
        
        //in unity wire to 
        //Placement Actor
        
        //we don't have to wire it thorugh the menu actor because it is
        //completely contextless - I even don't want it to close on Esc
        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Open(IContentNavigator navigator)
        {
            contentNavigator = navigator;
            navigator.OnChanged += Refresh;
            Refresh();
            Open();
        }

        public void Close()
        {
            if(contentNavigator != null) contentNavigator.OnChanged -= Refresh;
            gameObject.SetActive(false);
        }

        public void Refresh()
        {
            int middle = slots.Length / 2;
            for (int i = 0; i < slots.Length; i++)
            {
                int offset = i - middle;
                var entry = contentNavigator.GetRelative(offset);
                slots[i].text = entry?.displayName?? string.Empty;
                
                slots[i].color = offset == 0 ? Color.indianRed : Color.white;
                    
            }
        }
        
        

        public event Action<IMenu> OnCloseRequested;
        public event Action<IMenu> OnOpenRequested;

    }
}