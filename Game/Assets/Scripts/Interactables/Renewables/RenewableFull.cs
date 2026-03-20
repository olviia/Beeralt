using Actor;
using Interactables;
using Items;
using UnityEngine;


public class RenewableFull : MonoBehaviour, IRenewableState
{
    public GameObject uiPrompt;
    
    public void OnPlayerEnter(IGameActor player, RenewableObject context)
    {
        //register itself with a playuer
        player.SetCurrentInteractable(context);
        
        //todo:
        //show ui prompt facing the player 
        
        uiPrompt.SetActive(true);
    }

    public void OnPlayerExit(IGameActor player, RenewableObject context)
    {
        //unregister itself
        player.ClearCurrentInteractable(context);
        uiPrompt.SetActive(false);
    }

    public IRenewableState Interact(IGameActor gameActor,  RenewableObject context)
    {
        //give pollen to player inventory

        if (gameActor is IInventoryHolder inventoryHolder)
        {
            uiPrompt.SetActive(false);
            inventoryHolder.PassToInventory(context.item);
            return context.emptyObject;
        }
        else return context.fullObject;
    }
}
