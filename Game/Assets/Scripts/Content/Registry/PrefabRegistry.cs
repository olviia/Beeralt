using System.Collections.Generic;
using UnityEngine;

namespace Content.Registry
{
    [CreateAssetMenu(fileName = "PrefabRegistry", menuName = "ScriptableObjects/Storage/PrefabRegistry")]
    public class PrefabRegistry: ScriptableObject
    {
        //we are using list here because unity cannot serialize dictionaries
        //this is for serialisation
        [SerializeField] private List<PrefabEntry> prefabs;
        
        //redo list into dictionary for runtime
        private Dictionary<string, GameObject> prefabLookup = new();

        
        //this is to return the list after initialization, not the raw serialized data
        private List<PrefabEntry> afterInitializationPrefabs = new();
        public IReadOnlyList<PrefabEntry> GetPrefabs() => afterInitializationPrefabs;

        
        //for normal use
        //pattern some sort of injection, testability principle
        public void Initialize()
        {
            Initialize(prefabs);
        }
        
        //for testing purposes or to inject data
        public void Initialize(List<PrefabEntry> entries)
        {
            //clear dictionary just in case it was accidentally populated already
            prefabLookup.Clear();
            afterInitializationPrefabs.Clear();
            foreach (var entry in entries)
            {
                if (prefabLookup.ContainsKey(entry.id))
                {
                    Debug.LogWarning($"Prefab {entry.id} already exists in a PrefabRegistry dictionary. Skipping current entry");
                    continue;
                }
                prefabLookup.Add(entry.id, entry.prefab);
                afterInitializationPrefabs.Add(entry);
            }
        }

        public GameObject GetPrefab(string id)
        {
            if (prefabLookup.TryGetValue(id, out var prefab))
            {
                return prefab;
            }
            else
            {
                Debug.LogWarning ($"Prefab {id} does not exist in a PrefabRegistry dictionary");
                return null;
            }
        }
        
        
    }
}