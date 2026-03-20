using UnityEngine;

namespace Actor.Movement
{
    public class KinematicMovementStrategy:IMovementStrategy
    {
        private Rigidbody rigidbody;
        private Transform transform;
        float speed;
        private float yaw;
        private float pitch;

        public KinematicMovementStrategy(Rigidbody rigidbody, float speed)
        {
            this.rigidbody = rigidbody;
            this.transform = rigidbody.transform;
            this.speed = speed;
        }
        public void MoveForward()
        {
            transform.Translate(speed * Time.deltaTime* Vector3.forward);
        }

        public void MoveBackward()
        {
            transform.Translate(-speed * Time.deltaTime* Vector3.forward);
        }

        public void MoveLeft()
        {
            transform.Translate(-speed * Time.deltaTime* Vector3.right);
        }

        public void MoveRight()
        {
            transform.Translate(speed * Time.deltaTime* Vector3.right);
        }

        public void MoveUp()
        {
            transform.Translate(speed * Time.deltaTime* Vector3.up);
        }

        public void MoveDown()
        {
            transform.Translate(-speed * Time.deltaTime* Vector3.up);
        }

        public void OnEnter()
        {
            rigidbody.isKinematic = true;
        }

        public void OnExit()
        {
            rigidbody.isKinematic = false;
        }

        public void Rotate(float horizontal, float vertical)
        {
            yaw += horizontal;
            pitch -= vertical;
            pitch = Mathf.Clamp(pitch, -89, 89); 
            rigidbody.transform.rotation = Quaternion.Euler(pitch, yaw, 0.0f);
        }
        public Vector3 GetPassiveForce() => Vector3.zero;
    }
}