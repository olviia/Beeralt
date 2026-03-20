using System.Collections.Generic;
using Commands;
using UnityEngine;

namespace Input
{
    //I use interface to use different types of input
    public interface IInputSource
    {
        //kind of binding of keys or controller positions to 6dof
        public List<IMovementCommand> GetMovementInput();
        public List<IMetaCommand> GetMetaInput();
        public List<IActionCommand> GetActionInput();
        public List<IPlacementCommand> GetPlacementInput();
        public Vector2 GetRotation();
        public void ResetCursor();
        
        
        //for automatic input source switching
        event System.Action<IInputSource> OnBecameActive;
        
        
    }
}
