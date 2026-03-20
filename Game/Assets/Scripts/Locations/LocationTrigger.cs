using UnityEngine;

namespace Locations
{
    public class LocationTrigger:MonoBehaviour
    {
        [SerializeField] private LocationData locationData;
        void OnTriggerEnter(Collider collision)
        {
            if (collision.GetComponent<ILocationVisitor>() != null)
            {
                var actor = collision.GetComponent<ILocationVisitor>();
                actor.LocationVisited(locationData);
            }
        }

        void OnTriggerExit(Collider collision)
        {
            if (collision.GetComponent<ILocationVisitor>() != null)
            {
                var actor = collision.GetComponent<ILocationVisitor>();
                actor.LocationLeft(locationData);
            }
        }
    }
}