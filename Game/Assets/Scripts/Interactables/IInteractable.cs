using Actor;

namespace Interactables
{
    //for different types of interactions (polymorphism)
    public interface IInteractable
    {
        
        //mostly for the player to pass itself to the interactable
        void Interact(IGameActor player);

    }
}