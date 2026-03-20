using UnityEngine;

namespace Actor.Movement
{
    public class FlyingStrategy:IMovementStrategy
    {
        Rigidbody rigidbody;
        private float speed;

        private float yaw;
        private float pitch;

        private Vector3 hoverForce;
        //public Vector3 GetPassiveForce() => 
        public FlyingStrategy(Rigidbody rigidbody,  float speed)
        {
            this.rigidbody = rigidbody;
            this.speed = speed;
            hoverForce = (-Physics.gravity.y * rigidbody.mass)* Vector3.up;
        }


        public void MoveForward()
        {
            rigidbody.AddRelativeForce(Vector3.forward * (speed));
        }

        public void MoveBackward()
        {
            rigidbody.AddRelativeForce(Vector3.forward * (-speed));
        }

        public void MoveLeft()
        {
            rigidbody.AddRelativeForce(Vector3.right * -speed); 
        }

        public void MoveRight()
        {
            rigidbody.AddRelativeForce(Vector3.right * speed);
        }

        public void MoveUp()
        {
            rigidbody.AddForce(Vector3.up * speed);
        }

        public void MoveDown()
        {
            rigidbody.AddForce(Vector3.up * -speed);
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }

        public void Rotate(float horizontal, float vertical)
        {
            //rotate whole body here
            yaw += horizontal;
            pitch -= vertical;

            
            pitch = Mathf.Clamp(pitch, -89, 89); // clamped so player doesnt flip upside down
            //to directly set rotation and not fight with engine physics, i froze rotation for the rigidbody
            //i rotate around the parent of the bee so i can add drops and shift bee's position
            //inside of the parent components
            rigidbody.transform.rotation = Quaternion.Euler(pitch, yaw, 0.0f);
        }
        public Vector3 GetPassiveForce() => hoverForce;
    }
}