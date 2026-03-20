using Items;
using UnityEngine;

namespace Quests
{
    [CreateAssetMenu(fileName = "CollectObjective", menuName = "ScriptableObjects/Quest/Collect")]

    public class CollectObjective: QuestObjective

    {
        //I dont want objectives to be edited in runtime, only in inspector, 
        //and also I don't want anything else to see the objective. other scripts only
        //need to know if it is completed or not
        [SerializeField] private ItemData collectable;
        [SerializeField] private int amount;
        
        public int Amount => amount;
        public override bool? CheckItemCollected(ItemData item, int currentAmount)
        {
            if (collectable == item)
            {
                if (currentAmount >= amount)
                    return true;
                return false;
            }
    
            return null;
        }
        
        //method to return a description of an objective
        public override string GetDescription(int currentAmount)
        {
            return "Collect " + collectable.ItemName + (amount>1?"s: ":": ") + currentAmount + "/" + amount;
        }

        public override string GetDescription()
        {
            return GetDescription(0);
        }
    }
}