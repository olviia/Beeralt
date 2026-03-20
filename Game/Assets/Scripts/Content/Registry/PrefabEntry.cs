using System;
using UnityEngine;

namespace Content.Registry
{
    //says to unity that we want to be able to edit it in editor
    [Serializable]
    public struct PrefabEntry
    {
        public string id;
        public GameObject prefab;
        public string displayName;
    }
}