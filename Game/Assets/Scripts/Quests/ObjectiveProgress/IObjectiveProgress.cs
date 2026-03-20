namespace Quests.ObjectiveProgress
{
    
    //this is a thing that is passed to quest UI. This and only  this thing is passed there, so
    //nothing else is tightly coupled between quests and ui except progress structs and
    // questmediator.
    public interface IObjectiveProgress
    {
        public QuestObjective Objective { get; }
        
        bool IsCompleted{get; }
        
        public string GetDisplayText();
    }
}