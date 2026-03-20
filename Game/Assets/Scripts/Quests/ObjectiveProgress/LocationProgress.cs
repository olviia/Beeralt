namespace Quests.ObjectiveProgress
{
    public readonly struct LocationProgress:IObjectiveProgress
    {
        public QuestObjective Objective { get; }
        public bool IsCompleted { get; }

        public string GetDisplayText() => Objective.GetDescription();

        public LocationProgress(LocationObjective objective, bool isCompleted)
        {
            Objective = objective;
            IsCompleted = isCompleted;
        }
        
    }
}