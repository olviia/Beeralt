using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Quest/Quest",  order = 0)]

    public class Quest : ScriptableObject
    {
        [SerializeField] private string description;
        
        //list of all subquests that are required to complete the quest
        [SerializeField] private List<QuestObjective> objectives;
        

        
        //we don't want anything to change our objectives except designer in inspector
        public IReadOnlyList<QuestObjective> Objectives => objectives;

        public string GetDescription => description;

        public void AddObjective(QuestObjective objective)
        {
            objectives.Add(objective);
        }

        public void ReplaceObjective(QuestObjective currentObjective, QuestObjective newObjective)
        {
            objectives[objectives.IndexOf(currentObjective)] =  newObjective;
        }
    }
}