using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
    public class ItemData : ScriptableObject
    {
        [SerializeField]
        private string itemName;
        
        //property to get name, so I can set the name in inspector only, and then it cannot be changed outside of this class
        public string ItemName => itemName;

    }
}