using System;
using System.Collections.Generic;
using Input;
using Input.Contexts;
using UI;
using UnityEngine;

namespace Modes.EditMode
{
    public class EditModeController:MonoBehaviour, IMenu, IEditModeHost
    {
        //we get the reference to input manager here to push the context changes
        [SerializeField] InputManager inputManager;
        
        //here put IEditModeParticipant - what has to be opened when 
        //we enter edit mode
        [SerializeField] List<MonoBehaviour> editModeParticipants;
        public void Open()
        {
            inputManager.PushContext(new EditContext());
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("Edit mode opened");
            foreach (var participant in editModeParticipants)
            {
                if(participant is IEditModeParticipant item)item.OnEnterEditMode();
            }
        }

        public void Close()
        {
            inputManager.PopContext();
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("Edit mode closed");
            foreach (var participant in editModeParticipants)
            {
                if(participant is IEditModeParticipant item)item.OnExitEditMode();
            }
        }

        //if we close this menu with a ui button
        public event Action<IMenu> OnCloseRequested;
        public event Action<IMenu> OnOpenRequested;
        public void RequestClose() => OnCloseRequested?.Invoke(this);

    }
}