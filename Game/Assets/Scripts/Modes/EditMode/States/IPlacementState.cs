using UnityEngine;

namespace Modes.EditMode.States
{
    //this is a state machine for edit mode
    //that controls if we can place something,
    //if something is placed or if something is selected
    //to be moved, changed or placed
    //it is kind of implementing IPlacementActor and forces all the states 
    //to have same methods for an actor to call them
    
    public interface IPlacementState
    {
        void OnEnter();
        void OnExit();
        void Update(RaycastHit hit, bool isHit);
        void PlaceMove();
        void Scroll(int direction);
        void Inspect();
    }
}