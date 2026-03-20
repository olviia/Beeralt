using System;
using Content.ObjectsThatExistOnScene;
using Content.Registry;
using UnityEngine;

namespace SceneSetup
{
    //takes prefabs from PlacedData deserialized json
    //and initializes the instances from prefab registry
    public class WorldPopulator:MonoBehaviour
    {
        [SerializeField] private PrefabRegistry prefabRegistry;
        private PlacedData placedData;

        private void Start()
        {
            //null the position just in case
            transform.position = Vector3.zero;
            
            placedData = PlacedData.Load();
            foreach (var item in placedData.PlacedInstances)
            {
                var newObject = Instantiate(prefabRegistry.GetPrefab(item.prefabID),  
                                item.position, item.rotation,transform);
                newObject.transform.localScale = item.scale;
                var marker = newObject.AddComponent<PlacedObjectMarker>();
                marker.Initialize(newObject,  item.prefabID);
            }
            
        }
    }
}