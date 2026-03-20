using System.Collections.Generic;
using Commands;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class KeyboardInputSource : IInputSource
    {
        //for camera sensitivity
        public float mouseSensitivity = 0.5f;
        public bool skipRotationNextFrame;

        public void ResetCursor()
        {
            Mouse.current.WarpCursorPosition(new Vector2(Screen.width / 2f, Screen.height / 2f));
            //if we close menu, we don't want to keep the mouse delta 
            //for the bee to rotate immediatelly
            skipRotationNextFrame = true;
        }

        public event System.Action<IInputSource> OnBecameActive;
        Keyboard kb = new ();

        List<IMovementCommand> movementCommandList = new();
        List<IMetaCommand> metaCommandList = new();
        List<IActionCommand> actionCommandList = new();
        List<IPlacementCommand> placementCommandList = new();
        
        MoveForward moveForward = new ();
        MoveBackward moveBackward = new ();
        MoveLeft moveLeft = new ();
        MoveRight moveRight = new ();
        MoveUp moveUp = new ();
        MoveDown moveDown = new ();
        Dash dash = new ();
        Interact interact = new ();
        ToggleEditMode toggleEdit = new();
        Inspect inspect = new();
        
        PlaceMove placeMove = new ();
         
        
        Exit exit = new (); //for escape

        public List<IMovementCommand> GetMovementInput()
        {
            
            movementCommandList.Clear(); //to start with an empty list
            SetActive();
            

            if (kb.wKey.isPressed) movementCommandList.Add(moveForward);
            if (kb.sKey.isPressed) movementCommandList.Add(moveBackward);
            if (kb.aKey.isPressed) movementCommandList.Add(moveLeft);
            if (kb.dKey.isPressed) movementCommandList.Add(moveRight);
            if (kb.spaceKey.isPressed) movementCommandList.Add(moveUp);
            if (kb.leftShiftKey.isPressed) movementCommandList.Add(moveDown);
            
            return movementCommandList;
        }

        public List<IMetaCommand> GetMetaInput()
        {
            metaCommandList.Clear();
            SetActive();
            
            if (kb.escapeKey.wasReleasedThisFrame) metaCommandList.Add(exit);
            if (kb.tabKey.wasReleasedThisFrame) metaCommandList.Add(toggleEdit);
            
            return metaCommandList;
        }

        public List<IActionCommand> GetActionInput()
        {
            actionCommandList.Clear();
            SetActive();
            
            if (kb.eKey.isPressed) actionCommandList.Add(interact);
            if (kb.fKey.isPressed) actionCommandList.Add(dash);
            return actionCommandList;
        }

        public List<IPlacementCommand> GetPlacementInput()
        {
            placementCommandList.Clear();
            SetActive();
            var scroll = Mouse.current.scroll.ReadValue().y;
            if(scroll != 0) placementCommandList.Add(new Scroll(scroll));
            if(Mouse.current.leftButton.wasReleasedThisFrame) placementCommandList.Add(placeMove);
            if(Mouse.current.rightButton.wasReleasedThisFrame) placementCommandList.Add(inspect);
            
            return placementCommandList;
        }

        public Vector2 GetRotation()
        {
            if (skipRotationNextFrame)
            {
                skipRotationNextFrame = false;
                return Vector2.zero;
            }
            return Mouse.current.delta.ReadValue() * mouseSensitivity;
        }

        private void SetActive()
        {
            kb = Keyboard.current;
            // Fire event if any input detected
            if (kb.anyKey.isPressed || Mouse.current.delta.ReadValue() != Vector2.zero)
            {
                OnBecameActive?.Invoke(this);
            }

        }

    }
}
