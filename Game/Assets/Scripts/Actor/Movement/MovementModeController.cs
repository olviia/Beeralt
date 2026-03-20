using System;
using Helpers;
using Modes.EditMode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Actor.Movement
{
    //wire to edit mode controller on the scene
    //wire player rigidbody
    public class MovementModeController: MonoBehaviour, IEditModeParticipant
    {
        private KinematicMovementStrategy kinematicMovementStrategy;
        private WalkingStrategy walkingStrategy;
        private FlyingStrategy flyingStrategy;
        
        //here i decide what is current camera
        [SerializeField] private CameraPriority cameraPriority;
        [SerializeField] private float landingDistance = 0.5f;

        [SerializeField] private LayerMask landingMask;
        //maybe move it to some const file one day
        private const float editModeSpeedMultiplier = 20f;
        
        public IMovementStrategy activeMovementStrategy { get; private set; }
        
        private Rigidbody playerRigidbody;
        private float speed;
        private Collider playerCollider;

        //pass the info from player actor
        public void Initialize(Rigidbody playerRigidbody, GameObject head,  float speed)
        {
            this.playerRigidbody = playerRigidbody;
            this.speed = speed;
            //make sure the collider is on the player
            playerCollider = playerRigidbody.GetComponent<Collider>();
            kinematicMovementStrategy = new KinematicMovementStrategy(playerRigidbody, speed * editModeSpeedMultiplier);
            walkingStrategy = new WalkingStrategy(playerRigidbody, head, speed,landingMask);
            flyingStrategy = new FlyingStrategy(playerRigidbody, speed);
            activeMovementStrategy = flyingStrategy;

            walkingStrategy.OnStartFlying += StartFlying;
        }

        public void TryLand(Vector3 direction)
        {
            Debug.DrawRay(playerRigidbody.transform.position, direction*landingDistance, Color.yellow);

            if (activeMovementStrategy != flyingStrategy) return;
            else if (Physics.Raycast(playerRigidbody.position, direction, out RaycastHit hit, landingDistance, landingMask))
            {
                if(hit.collider)
                walkingStrategy.SetSurfaceNormal(hit.normal, hit.point);
                SwitchTo(walkingStrategy);
                cameraPriority.SetWalkCameraAsCurrent();
                Debug.Log("Landing");
            }
        }

        public void StartFlying()
        {
            SwitchTo(flyingStrategy);
            cameraPriority.SetFlyCameraAsCurrent();
        }

        private void SwitchTo(IMovementStrategy newStrategy)
        {
            activeMovementStrategy.OnExit();
            activeMovementStrategy = newStrategy;
            activeMovementStrategy.OnEnter();
            
        }


        public void OnEnterEditMode()
        {
            SwitchTo(kinematicMovementStrategy);
            cameraPriority.SetEditCameraAsCurrent();
        }

        public void OnExitEditMode()
        {
            SwitchTo(flyingStrategy);
            cameraPriority.SetFlyCameraAsCurrent();
        }
    }
}