using System.Collections.Generic;
using Quests.ObjectiveProgress;
using UnityEngine;
using UnityEngine.UI;

namespace Quests.QuestUI
{
    //this class sits on thescene, on the UI element that is responsible for Quest system
    public class QuestUIController:MonoBehaviour
    {
        [SerializeField] public QuestMediator questMediator;
        
        [SerializeField] private QuestEntryUI questEntryUIPrefab;
        [SerializeField] private ObjectiveEntryUI objectiveEntryUIPrefab;
        
        private Dictionary<Quest, QuestEntryUI> quests = new();
        
        private Dictionary<(Quest, QuestObjective), ObjectiveEntryUI> objectives = new();

        private void Awake()
        {
            questMediator.OnObjectiveChanged += ChangeObjective;
            questMediator.OnQuestCompleted += CompleteQuest;
            questMediator.OnQuestStarted += AddQuest;
            
            //add initial quests
            foreach (var quest in questMediator.Quests)
            {
                AddQuest(quest);
            }
        }

        private void ChangeObjective(IObjectiveProgress progress, Quest quest)
        {
            //here we receive all the quests with the same objectives from Quest Mediator, so no
            //need to go through the individual objectives as a partial key
            if (objectives.ContainsKey((quest, progress.Objective)))
            {
                string text = progress.GetDisplayText();
                
                objectives[(quest, progress.Objective)].SetText(text);
                if (progress.IsCompleted)
                {
                    objectives[(quest, progress.Objective)].MarkComplete();
                }
                else
                {
                    objectives[(quest, progress.Objective)].MarkNotComplete();
                }
            }
        }

        private void AddQuest(Quest quest)
        {
            QuestEntryUI newQuest = Instantiate(questEntryUIPrefab, transform);
            quests.Add(quest, newQuest);
            newQuest.SetText(quest.GetDescription);
            
            foreach (QuestObjective objective in quest.Objectives)
            {
                ObjectiveEntryUI newObjective = Instantiate(objectiveEntryUIPrefab, newQuest.transform);
                objectives.Add((quest, objective), newObjective);
                newObjective.SetText(objective.GetDescription());
                
                //rebuild child first, so parent knows about its size
                LayoutRebuilder.ForceRebuildLayoutImmediate(newObjective.transform as RectTransform);
            }
            //this is for vertical layout group to be rerendered based on new size of text,
            //quests, and objectives
            LayoutRebuilder.ForceRebuildLayoutImmediate(newQuest.transform as RectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(this.transform as RectTransform);
        }

        private void CompleteQuest(Quest quest)
        {
            quests[quest].MarkComplete();
        }
        
        
        private void OnDestroy()
        {
            questMediator.OnObjectiveChanged -= ChangeObjective;
            questMediator.OnQuestCompleted -= CompleteQuest;
            questMediator.OnQuestStarted -= AddQuest;
        }
    }
}