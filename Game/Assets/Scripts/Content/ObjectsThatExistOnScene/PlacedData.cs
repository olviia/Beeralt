using System;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace Content.ObjectsThatExistOnScene
{
    [Serializable]
    public class PlacedData
    {
        [SerializeField] private List<PlacedInstance> placedInstances = new();
        
        public IReadOnlyList<PlacedInstance> PlacedInstances => placedInstances;
        
        public void Add(PlacedInstance instance) => placedInstances.Add(instance);
        public void Remove(PlacedInstance instance) => placedInstances.Remove(instance);
        public void Clear() => placedInstances.Clear();
        
        public void Save() => SaveSystem.Save(SavePaths.savedObjects, this);
        
        public static PlacedData Load() => SaveSystem.Load<PlacedData>(SavePaths.savedObjects);
        
    }
}