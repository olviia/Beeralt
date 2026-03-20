using UnityEngine;

namespace Actor.Movement
{
    public interface IMovementStrategy
    {
        void MoveForward();
        void MoveBackward();
        void MoveLeft();
        void MoveRight();
        void MoveUp();
        void MoveDown();
        void OnEnter();
        void OnExit();
        void Rotate(float horizontal, float vertical);
        //for gravity in hovering, for adhesive force in walking
        Vector3 GetPassiveForce();
    }
}