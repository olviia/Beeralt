using System;
using System.Collections.Generic;
using Content.Registry;
using UnityEngine;

namespace Content.ObjectsThatExistOnScene
{
    public class FlatListNavigator:IContentNavigator
    {
        IReadOnlyList<PrefabEntry> prefabEntries;


        public bool TryEnter() => false;
        public bool TryLeave() => false;
        public bool IsAtLeaf => true;
        public bool IsAtRoot => true;
        public int currentIndex { get; set; }

        //currently it will be the
        public string CurrentDisplayName => prefabEntries[currentIndex].displayName;
        public string CurrentPrefabID => prefabEntries[currentIndex].id;
        public GameObject CurrentPrefab => prefabEntries[currentIndex].prefab;
        
        public (string displayName, string prefabID)? GetRelative(int offset)
        {
            
            //using wrap as scroll already implements
            int count = prefabEntries.Count;
            int index = (currentIndex + offset + count) % count;
            var entry = prefabEntries[index];
            return(entry.displayName, entry.id);
        }

        public event Action OnChanged;

        //we pass here the prefabs because we should not know where we read the data
        //from. we want more decoupling, so we read whatever is passed in the constructor.
        //for example, to upload user modified data, we will use jsons
        public FlatListNavigator(IReadOnlyList<PrefabEntry> prefabs)
        {
            //this is for development purposes. in the final build redesign it as it is
            //a bad practice to deliberately throw exceptions.
            if (prefabs == null || prefabs.Count == 0)
                throw new
                    ArgumentException("FlatListNavigator requires at least one entry.");

            prefabEntries = prefabs;
            
        }
        
        public void Scroll(int direction)
        {
            int count = prefabEntries.Count;
            
            //no scroll when there is nothing to scroll
            if (count == 0) return;
            
            //some wrap around algorithm
            currentIndex = (currentIndex + direction + count) % count;
            
            //here select prefab by index
            
            OnChanged?.Invoke();
        }
    }
}