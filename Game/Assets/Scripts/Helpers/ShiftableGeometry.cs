using UnityEngine;

namespace Helpers
{
    //this component lives on the object that can be shifted on trigger
    //of it's parent with the object that is capable of shifting geometry
    public class ShiftableGeometry:MonoBehaviour
    {
        private Vector3 basePosition;

        private void Start()
        {
            basePosition = this.transform.localPosition;
        }

        public void Shift(Vector3 direction)
        {
            transform.localPosition = basePosition + direction;
        }

        public void Reset()
        {
            transform.localPosition = basePosition;
        }
    }
}