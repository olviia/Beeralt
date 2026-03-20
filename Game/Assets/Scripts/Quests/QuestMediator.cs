using System;
using System.Collections.Generic;
using Actor;
using Items;
using Locations;
using Quests.ObjectiveProgress;
using UnityEngine;

namespace Quests
{
    
    //this class has to live on the scene for it to allow the communication between 
    //quests, inventory and ui
    //also it checks quests completion
    //quest givers should reference this script through the inspector
    //the methods for quest operations are provided
    
    public class QuestMediator:MonoBehaviour
    {
        
        //hard coded game actor as I can't serialize interfaces
        //further validation is performed in the editor
        [SerializeField] private GameObject player;
        private IGameActor gameActor;
        private Inventory inventory;
        
        //events for UI
        public event Action<Quest> OnQuestStarted;
        
        //here i will pass the objective and the current amount in invetory
        public event Action<IObjectiveProgress, Quest> OnObjectiveChanged;
        public event Action<Quest> OnQuestCompleted;
        
        //we can assign initial quests
        [SerializeField]private List<Quest> quests;
        //ireadonly so nobody can change it
        public IReadOnlyList<Quest> Quests => quests;
        
        //there has to be a hash set of completed objectives. and if the requirements not met anymore,
        //the objective is removed from this list
        private HashSet<(Quest,QuestObjective)> completedObjectives = new ();
        

        private void Awake()
        {
            gameActor = player.GetComponent<IGameActor>();
            //get reference to the correct inventory for the correct actor
            if(gameActor is IInventoryHolder inventoryHolder)
            {
                inventory = inventoryHolder.GetInventoryReference();

                //i don't need here two different events like OnItemAdded and OnItemRemoved, 
                //because I always pass how many items are currently in the inventory
                
                inventory.OnInventoryChanged += HandleInventoryChange;

            }

            if (gameActor is ILocationVisitor locationVisitor)
            {
                //here i need two events, because it swaps the boolean in the check
                locationVisitor.OnLocationVisited += HandleLocationVisited;
                locationVisitor.OnLocationLeft += HandleLocationLeft;
            }
        }

        private void HandleInventoryChange(ItemData item, int amount)
        {
            //lambdaaaaaaaaa my first one
            EvaluateObjective(objective => objective.CheckItemCollected(item, amount),
                            (objective, completed) => new CollectProgress((CollectObjective)objective,completed, amount));
        }

        private void HandleLocationVisited(LocationData location)
        {
            //i just don't understand lambdas it was autocomplete
            EvaluateObjective(objective => objective.CheckLocationReached(location),
                            (objective,completed) => new LocationProgress((LocationObjective)objective,completed));
            
        }
        
        //check if this one is correct for the location unvisited. here i swap the 
        //returning result from lambda from true to false so we check if we leave the location
        //in onTriggerExit of LocationTrigger
        //In other words: If the location matches, return   
        //false (remove it). Otherwise, return null (don't touch).
        private void HandleLocationLeft(LocationData location)
        {
            EvaluateObjective(objective => objective.CheckLocationReached(location) == true?false:null, 
                (objective,completed) => new LocationProgress((LocationObjective)objective,completed));
        }

        //here func is delegate, in other words it is a signature of a function that i am going
        //to pass here. < here are all the incoming arguments , here is what funtion returns>
        //so first, we pass the QuestObjective to be evaluated if it is completed or not
        //and second we pass a constructor for a IObjectiveProgress so we can later pass it in
        //the event for ui
        private void EvaluateObjective(Func<QuestObjective, bool?> condition,
                                    Func<QuestObjective, bool, IObjectiveProgress> createProgress)
        {
            foreach(Quest quest in quests)
            {
                foreach (QuestObjective objective in quest.Objectives)
                {
                    bool? isCompleted = condition(objective);
                    
                    if(isCompleted == true)
                        completedObjectives.Add((quest, objective));
                    
                    else if(isCompleted == false)
                        completedObjectives.Remove((quest, objective));

                    if(isCompleted != null) 
                       OnObjectiveChanged?.Invoke(createProgress(objective, (bool)isCompleted), quest);
                }
                CheckQuestCompletion(quest);
            }

        }
        
        public void StartQuest(Quest quest)
        {
            //for the trigger elements to call this method, saying
            //that this quest became active
            quests.Add(quest);
            OnQuestStarted?.Invoke(quest);
            
            //currently I assume that the player starts with an empty inventory and not stands in
            //the trigger location for any active quests
            //additional refactoring needed to cover those edge cases as 
            //well as adding the save file
        }

        public bool CheckQuestCompletion(Quest quest)
        {
            foreach (QuestObjective objective in quest.Objectives)
            {
                if (!completedObjectives.Contains((quest, objective)))
                    return false;
            }

            Debug.Log("Quest completed: " + quest.GetDescription);
            
            //remove from list with objectives
            foreach (QuestObjective objective in quest.Objectives)
            {
                completedObjectives.Remove((quest, objective));
            }
            OnQuestCompleted?.Invoke(quest);
            return true;
        }

        private void OnDestroy()
        {
            if (gameActor is IInventoryHolder inventoryHolder)
            {
                inventory.OnInventoryChanged -= HandleInventoryChange;
            }
            if (gameActor is ILocationVisitor locationVisitor)
            {
                locationVisitor.OnLocationVisited -= HandleLocationVisited;
                locationVisitor.OnLocationLeft -= HandleLocationLeft;
            }
        }
    }
}