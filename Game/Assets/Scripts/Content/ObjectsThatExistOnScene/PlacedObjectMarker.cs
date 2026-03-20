using UnityEngine;

namespace Content.ObjectsThatExistOnScene
{
    //class to say that this object was placed and can be moved in edit mode
    public class PlacedObjectMarker:MonoBehaviour
    {
        public PlacedInstance placedInstance { get;  private set; }

        public void Initialize(GameObject prefab, string id)
        {
            placedInstance = new PlacedInstance()
            {
                prefabID =  id,
                position = prefab.transform.position,
                rotation = prefab.transform.rotation,
                scale = prefab.transform.localScale,
            };
        }

        public void Place(GameObject gameObject)
        {
            placedInstance.position = gameObject.transform.position;
            placedInstance.rotation = gameObject.transform.rotation;
            placedInstance.scale = gameObject.transform.localScale;
        }
        
    }
}