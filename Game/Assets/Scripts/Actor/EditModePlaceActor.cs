using System;
using Content.ObjectsThatExistOnScene;
using Content.Registry;
using Input;
using Input.Contexts;
using Modes.EditMode;
using Modes.EditMode.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Actor
{
    //this class is for processing the commands for placement
    public class EditModePlaceActor:MonoBehaviour, IPlacementActor, IEditModeParticipant
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private PrefabRegistry prefabRegistry;
        [SerializeField] private Camera cam;
        [SerializeField] private EditModeInspectMenu inspectMenu;
        [SerializeField] private EditModeDisplaySelectionMenu displaySelectionMenu;
        
        
        //for saving or loading objects on scene
        private PlacedData placedData;
        
        //how far we can place
        private const float distance = 20f;
        
        //for development it is FlatListNavigator
        private IContentNavigator contentNavigator;
        private IPlacementState currentState;
        
        private GhostPlacingState ghostPlacingState;
        private NothingSelectedState nothingSelectedState;
        private ObjectMoveState objectMoveState;
        

        private bool isActive;

        void Start()
        {
            placedData = PlacedData.Load();
            contentNavigator = new FlatListNavigator(prefabRegistry.GetPrefabs());
            ghostPlacingState = new GhostPlacingState(contentNavigator, displaySelectionMenu);
            objectMoveState = new ObjectMoveState();
            
            nothingSelectedState = new NothingSelectedState();
            
            ghostPlacingState.OnInstancePlaced += placedData.Add;
            ghostPlacingState.OnCancelled += () => TransitionTo(nothingSelectedState);
            nothingSelectedState.OnScroll += () => TransitionTo(ghostPlacingState);
            nothingSelectedState.OnObjectSelected += OpenInspectMenu;
            //hehe first two liner lambda
            nothingSelectedState.OnObjectMoveRequested += marker =>
            {
                objectMoveState.Prepare(marker);
                TransitionTo(objectMoveState);
            };
            objectMoveState.OnFinishedMove += () => TransitionTo(nothingSelectedState);
        }

        void Update()
        {
            if (!isActive) return;
            
            var command = inputManager.GetPlacementInput(); 
            if (command != null)
            {
                foreach (var item in command)
                {
                    item.Execute(this);
                    Debug.Log(item.GetType().Name);
                }
            }
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            var isHit = Physics.Raycast(ray, out var currentHit, distance);
            
            currentState.Update(currentHit, isHit);
        }
        
        public void PlaceMove() => currentState.PlaceMove();

        public void Scroll(int direction)
        {
            currentState.Scroll(direction);
        }

        public void Inspect() => currentState.Inspect();
        

        public void TransitionTo (IPlacementState state)
        {
            if (currentState == state) return;
            currentState?.OnExit();
            currentState = state;
            currentState?.OnEnter();
        }

        public void OnEnterEditMode()
        {
            isActive = true;
            TransitionTo(ghostPlacingState);
            
        }

        public void OnExitEditMode()
        {
            isActive = false;
            currentState?.OnExit();
            
            currentState = null;
            
            //save here. maybe it is worth to save in runtime as well
            //this is a question for the future
            
            placedData.Save();
        }

        private void OpenInspectMenu(PlacedObjectMarker marker)
        {
            inspectMenu.Open(marker);
        }

        private void OnDestroy()
        {
            ghostPlacingState.OnInstancePlaced -= placedData.Add;
        }
    }
}