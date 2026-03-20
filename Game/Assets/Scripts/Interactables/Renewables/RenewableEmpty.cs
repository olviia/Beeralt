using Actor;
using Interactables;
using UnityEngine;

public class RenewableEmpty : MonoBehaviour, IRenewableState 
{
    public void OnPlayerEnter(IGameActor player, RenewableObject context)
    {
        //do nothing so don't register with a player
    }

    public void OnPlayerExit(IGameActor player, RenewableObject context)
    {
        //still do nothing
    }

    public IRenewableState Interact(IGameActor gameActor,  RenewableObject context)
    {
        //still do nothing, return empty
        return context.emptyObject;
    }
    
    //maybe regenerate something over time
}

