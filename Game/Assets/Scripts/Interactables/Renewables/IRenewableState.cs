using Actor;

namespace Interactables
{
    //sort of finite state machine for renewables.
    //for the other sort of interaction i might need to create another interface
    //maybe it should be in a subfolder
    

    public interface IRenewableState
    {
        void OnPlayerEnter(IGameActor player, RenewableObject context);
        void OnPlayerExit(IGameActor player, RenewableObject context);
        
        IRenewableState Interact(IGameActor player, RenewableObject context);
    }
}