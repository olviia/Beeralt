using Actor;
using Items;
using UnityEngine;

namespace Interactables
{
    public class RenewableObject: MonoBehaviour, IInteractable
    {
        public RenewableFull fullObject;
        public RenewableEmpty emptyObject;
        
        //what this object gives to player inventory
        //here we hardcode what item we give
        //maybe in the future we can randomize it for chests or something
        public ItemData item;
            

        private IRenewableState currentState;

        private void Awake()
        {
            //set desired state as the current state 
            //with the respected game object set active and others deactivated
            currentState = fullObject;
            fullObject.gameObject.SetActive(true);
            emptyObject.gameObject.SetActive(false);
        }

        void OnTriggerEnter(Collider collision)
        {
            if (collision.GetComponent<IGameActor>() != null) {
                currentState.OnPlayerEnter(collision.GetComponent<IGameActor>(), this);
            }
        }

        void OnTriggerExit(Collider collision)
        {
            if (collision.GetComponent<IGameActor>() != null) {
                currentState.OnPlayerExit(collision.GetComponent<IGameActor>(), this);
            }
        }
        

        public void Interact(IGameActor player)
        {
            var prevState = (MonoBehaviour)currentState;
            currentState = currentState.Interact(player, this);

                prevState.gameObject.SetActive(false);
                ((MonoBehaviour)currentState).gameObject.SetActive(true);
            
        }
    }
}
