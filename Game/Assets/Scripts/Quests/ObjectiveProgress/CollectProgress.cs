namespace Quests.ObjectiveProgress
{
    public readonly struct CollectProgress:IObjectiveProgress
    {
        public QuestObjective Objective { get; }
        public bool IsCompleted { get; }
        public string GetDisplayText() => Objective.GetDescription(CurrentAmount);

        public int CurrentAmount { get; }
        public int TargetAmount { get; }

        public CollectProgress(CollectObjective objective, bool isCompleted, int currentAmount)
        {
            Objective = objective;
            IsCompleted = isCompleted;
            CurrentAmount = currentAmount;
            TargetAmount = objective.Amount;
        }
    }
}