using UnityEngine;

namespace Locations
{
    [CreateAssetMenu(fileName = "Location", menuName = "ScriptableObjects/Location")]

    public class LocationData:ScriptableObject
    {
        [SerializeField]
        private string locationName;
        
        //property to get name, so I can set the name in inspector only, and then it cannot be changed outside of this class
        public string LocationName => locationName;
    }
}