using System;
using Interactables;

namespace Actor
{
    //for implementing the command pattern
    //we return what method the actor does from input source
    //and then execute it in an actor
    public interface IGameActor
    {
        void MoveForward();
        void MoveBackward();
        void MoveLeft();
        void MoveRight();
        void MoveUp();
        void MoveDown();
        void Dash();
        void Interact();
        void Attack();
        
        
        void SetCurrentInteractable(IInteractable interactable);
        void ClearCurrentInteractable(IInteractable interactable);
        public event Action OnCameraShaking;
    }
}