using System;
using UnityEngine;

namespace Content.ObjectsThatExistOnScene
{
    [Serializable]
    public class PlacedInstance
    {
        public string prefabID;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }
}