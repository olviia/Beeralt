using System;
using System.Collections.Generic;
using Actor.Movement;
using Attributes;
using Commands;
using Helpers;
using Input;
using Interactables;
using Items;
using Locations;
using Modes.EditMode;
using UnityEngine;

namespace Actor
{
    //This is a bee game actor 
    //in this class, i apply info from input to rigidbody
    //and utilize command pattern
    
    //wire MovementModeController on scene
    public class BeeGameActor : MonoBehaviour, IGameActor, 
                                IInventoryHolder, ILocationVisitor,
                                IHittable
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private Rigidbody playerRigidbody;

        [SerializeField] private GameObject playerHead;
        //here i control movement strategy
        [SerializeField] private MovementModeController movementModeController;
        
        //only one inventory can be created, and we cannot reassing a new inventory later
        public readonly Inventory inventory = new Inventory();
        //same with health
        [SerializeField] private Health health;
        
        //this is the force that counteracts gravity, so we can hover in place when nothing is pressed on keyboard
        //or keep walking on non horizontal surfaces
        private float passiveForce;
        
        //how fast player moves up/down/forward/backward
        public float maxMovementSpeed = 0.5f;
        
        public float maxDashSpeed = 2f;
        
        //accumulated rotation after mouse
        private float yaw;
        private float pitch;

        private List<IMovementCommand> movementCommand;
        private List<IActionCommand> actionCommand;
        private IInteractable currentInteractable;
        
        public event Action<LocationData> OnLocationVisited;
        public event Action<LocationData> OnLocationLeft;
        
        //this event signals when the camera is being shaked
        public event Action OnCameraShaking;


        void Start()
        {
            movementModeController.Initialize(playerRigidbody, playerHead, maxMovementSpeed);
        }
        void FixedUpdate()
        {
            //balance gravity or adhesion
            playerRigidbody.AddForce(movementModeController.activeMovementStrategy.GetPassiveForce());
            
            //execute actions in fixed update
            if (movementCommand != null)
            {
                foreach (var item in movementCommand)
                {
                    item.Execute(this);
                }
            }
            if (actionCommand != null)
            {
                foreach (var item in actionCommand)
                {
                    item.Execute(this);
                }
            }
        }

        void Update()
        {
            //get the frame based input in Update
            movementCommand = inputManager.GetMovementInput();
            actionCommand = inputManager.GetActionInput();
            
            //mouse input is updated every frame, so i don't put it in fixed update
            Vector2 rotation = inputManager.GetCameraRotation();

            //fire event that the camera is rotating quickly - shaking
            if (ShakeDetector.IsShaking(rotation))
                OnCameraShaking?.Invoke();
            
            movementModeController.activeMovementStrategy.Rotate(rotation.x, rotation.y);
        }

        public void MoveForward()
        {
            movementModeController.activeMovementStrategy.MoveForward();
        }

        public void MoveBackward()
        {
            movementModeController.activeMovementStrategy.MoveBackward();
        }

        public void MoveLeft()
        {
            movementModeController.activeMovementStrategy.MoveLeft();
        }

        public void MoveRight()
        {
            movementModeController.activeMovementStrategy.MoveRight();
        }

        public void MoveUp()
        {
            movementModeController.activeMovementStrategy.MoveUp();
        }

        public void MoveDown()
        {
            movementModeController.activeMovementStrategy.MoveDown();
        }


        public void Dash()
        {
            throw new System.NotImplementedException();
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void Interact()
        {
            if (currentInteractable != null) currentInteractable.Interact(this);
        }

        public void SetCurrentInteractable(IInteractable interactable)
        {
            currentInteractable = interactable;
        }        
        public void ClearCurrentInteractable(IInteractable interactable)
        {
            if(currentInteractable == interactable) currentInteractable = null;
        }

        public Inventory GetInventoryReference()
        {
            return inventory;
        }

        public void PassToInventory(ItemData item)
        {
            inventory.Add(item);
        }
        
        public void LocationVisited(LocationData location)
        {
            OnLocationVisited?.Invoke(location);
        }

        public void LocationLeft(LocationData location)
        {
            OnLocationLeft?.Invoke(location);
        }

        public void TakeHit(int damage)
        {
            health.Decrease(damage);
            Debug.Log($"Hit {damage} damage, health: {health.Current}/{health.Max}");
        }
    }
}
