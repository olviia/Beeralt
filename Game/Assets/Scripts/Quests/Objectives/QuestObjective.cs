using Items;
using Locations;
using UnityEngine;

namespace Quests
{
    public abstract class QuestObjective:ScriptableObject
    {
        //virtual means we do this method by default, but can override it
        public virtual bool? CheckItemCollected(ItemData item, int currentProgress) => null;
        
        //here bool? means that this method can actually return true, falce and null, 
        //so bool? is nullable
        public virtual bool? CheckLocationReached(LocationData location) => null;

        public virtual string GetDescription(int currentProgress) => "";
        
        //i am overriding it so i can get different description format out of different goal types
        public virtual string GetDescription() => "";

    }
}