using System;
using System.Collections;
using System.Collections.Generic;
using Input;
using Input.Contexts;
using Modes.EditMode;
using Quests;
using UI;
using UnityEngine;

namespace Actor
{
    public class GeneralMenuActor:MonoBehaviour,IMenuActor
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private QuestMediator questMediator;
        [SerializeField] private PauseMenu pauseMenu;
        
        //temporary call congratulations
        [SerializeField] private CongratsPopup congratsPopup;
        //like a whole edit mode is a menu, just another type of menu i suppose
        [SerializeField] private EditModeController editModeController;

        [SerializeField] private EditModeInspectMenu editModeInspectMenu;
        

        private Stack<IMenu> activeMenuStack = new();
        
        
        private Action<Quest> onQuestCompleted;

        private void Start()
        {
            //here subscribe to everything
            pauseMenu.OnCloseRequested += CloseMenu;
            //i cant unsubscribe, it is for demo
            onQuestCompleted = _ => OpenMenu(congratsPopup); 
            questMediator.OnQuestCompleted += onQuestCompleted;

            editModeInspectMenu.OnOpenRequested += OpenMenu;
            editModeInspectMenu.OnCloseRequested += CloseMenu;
                        
            //lock cursor here
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            //get the frame based input in Update
            var command = inputManager.GetMetaInput(); 
            if (command != null)
            {
                foreach (var item in command)
                {
                    item.Execute(this);
                }
            }
        }
        public void Exit()
        {
            if (activeMenuStack.Count> 0)
            {
                CloseMenu(activeMenuStack.Peek());
            }
            else
            {
                OpenMenu(pauseMenu);
            }
        }

        public void ToggleEditMenu()
        {
            //only open it when there are no other menus open
            if (activeMenuStack.Count == 0) OpenMenu(editModeController);
            else if(activeMenuStack.Peek() is EditModeController) CloseMenu(editModeController);
        }

        public void RightClickMenu()
        {
            //nothing yet
        }

        public void OpenMenu(IMenu menu)
        {
            menu.Open();
            activeMenuStack.Push(menu);
            if (menu is PausingMenu) inputManager.PushContext(new MenuContext());
            
        }

        public void CloseMenu(IMenu menu)
        {
            menu.Close();

            if (activeMenuStack.Peek() == menu) activeMenuStack.Pop();
            if (menu is PausingMenu)
            {
                
                inputManager.ResetCursor();
                inputManager.PopContext();
            }
        }

        private void OnDestroy()
        {
                pauseMenu.OnCloseRequested -= CloseMenu;
                questMediator.OnQuestCompleted -= onQuestCompleted;
                editModeInspectMenu.OnOpenRequested -= OpenMenu;
                editModeInspectMenu.OnCloseRequested -= CloseMenu;
        }
    }
}