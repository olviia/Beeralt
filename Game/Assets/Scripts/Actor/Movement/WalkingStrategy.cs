using System;
using UnityEngine;

namespace Actor.Movement
{
    public class WalkingStrategy:IMovementStrategy
    {
        Rigidbody rigidbody;
        GameObject head;
        private float speed;

        private float turnSpeed = 90; //degrees/second
        private float lerpSpeed = 10; //smoothing speed

        private float distGround;  //distance to the ground
        
        private Vector3 surfaceNormal;
        private Vector3 myNormal; //character normal
        private Quaternion targetRot; //where to rotate according to surface normal
        private Vector3 myForward; //where to apply force to
        private Vector3 hitPoint;
        private float gravityMagnitude;
        private LayerMask layerMask;
        private CapsuleCollider playerCollider;

        private Quaternion targetRotation;


        private const float lookAheadDistance = 0.8f;

        private float pitch;

        public event Action OnStartFlying;
        

        public WalkingStrategy(Rigidbody rigidbody,GameObject head, float speed, LayerMask layerMask)
        {
            this.rigidbody = rigidbody;
            this.speed = speed;
            this.head = head;
            this.layerMask = layerMask;
            gravityMagnitude = Physics.gravity.magnitude * rigidbody.mass;
            playerCollider = rigidbody.GetComponent<CapsuleCollider>();
        }

        public void CheckDirection(Vector3 direction)
        {
            Ray ray;
            RaycastHit hit;

            ray = new Ray(head.transform.position, -myNormal); //cust ray downward, cast if from the head
            
            Debug.DrawRay(head.transform.position, -myNormal*lookAheadDistance, Color.cornflowerBlue);

            if (Physics.Raycast(ray, out hit, lookAheadDistance, layerMask))
            {
                Debug.Log("hit: " + hit.collider.gameObject.name);
                surfaceNormal = hit.normal;
                targetRotation = Quaternion.FromToRotation(rigidbody.transform.up, hit.normal) * rigidbody.transform.rotation;
            }

            myNormal = Vector3.Lerp(myNormal, surfaceNormal, lerpSpeed * Time.deltaTime);
            //find forward direction with new myNormal
             myForward = Vector3.Cross(rigidbody.transform.right, myNormal);
            //align with the new normal keeping the forward direciton
            
        }
        public void MoveForward()
        {
            CheckDirection(-rigidbody.transform.up);
            rigidbody.AddForce(myForward * (speed));
            Debug.DrawRay(head.transform.position, rigidbody.transform.forward, Color.darkOrchid);
        }

        public void MoveBackward()
        {
            CheckDirection(-rigidbody.transform.forward);
            rigidbody.AddRelativeForce(Vector3.forward * (-speed));
        }

        public void MoveLeft()
        {
            CheckDirection(-rigidbody.transform.right);
            rigidbody.AddRelativeForce(Vector3.right * -speed);
        }

        public void MoveRight()
        {
            CheckDirection(rigidbody.transform.right);
            rigidbody.AddRelativeForce(Vector3.right * speed);
        }

        public void MoveUp()
        {
            // go into flying mode
            OnStartFlying?.Invoke();
        }

        public void MoveDown()
        {
           //stealth?
        }

        public void OnEnter()
        {
            //snap to surface
            rigidbody.useGravity = false;
            
            //here kinematic = true, rotate the bee, and then turn the kinematic back
            rigidbody.isKinematic = true;
            rigidbody.transform.rotation = Quaternion.FromToRotation(rigidbody.transform.up, surfaceNormal)*rigidbody.transform.rotation;
            rigidbody.isKinematic = false;
            
            //hard coded collider. if player changes collider, this has to be redone
            rigidbody.linearVelocity = Vector3.zero;
            rigidbody.MovePosition(hitPoint+surfaceNormal*(playerCollider.radius+0.1f)
                                   - rigidbody.rotation * playerCollider.center);

            targetRotation = rigidbody.transform.rotation;
            
            pitch = 0;
            head.transform.localRotation = Quaternion.identity;
            distGround = playerCollider.bounds.extents.y - playerCollider.center.y; //check if this is the correct way to find the distance to the ground for capsule collider
            myNormal = rigidbody.transform.up;                                                              //maybe we should use x and not y, or even z
        }

        public void OnExit()
        {
            rigidbody.useGravity = true;
        }

        public void Rotate(float horizontal, float vertical)
        {
            targetRot = Quaternion.LookRotation(
                Vector3.ProjectOnPlane(rigidbody.transform.forward, myNormal)
                    .normalized, myNormal);
            rigidbody.transform.rotation = Quaternion.Lerp(rigidbody.transform.rotation, targetRot, lerpSpeed * Time.deltaTime);
            
            
            pitch -= vertical;
            pitch = Mathf.Clamp(pitch, -89, 89);
            head.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
            
            var newRotation = Quaternion.AngleAxis(horizontal, myNormal);
            rigidbody.transform.rotation *= newRotation;
        }

        public void SetSurfaceNormal(Vector3 normal, Vector3 hitPoint)
        {
            surfaceNormal = normal;
            this.hitPoint = hitPoint;
        }
        
        
        public Vector3 GetPassiveForce()
        {
           return -gravityMagnitude* surfaceNormal;
           //return new Vector3(0,0,0);
        }
    }
}