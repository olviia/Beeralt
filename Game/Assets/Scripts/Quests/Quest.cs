using UnityEngine;

namespace Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Quest/Quest",  order = 0)]

    public class Quest : ScriptableObject
    {
        [SerializeField] private string description;
        
        //list of all subquests that are required to complete the quest
        [SerializeField] private QuestObjective[] objectives;
        
        //we don't want anything to change our objectives except designer in inspector
        public QuestObjective[] Objectives => objectives;
        public string GetDescription => description;
    }
}