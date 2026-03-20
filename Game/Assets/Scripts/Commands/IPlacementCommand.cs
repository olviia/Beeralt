using Actor;

namespace Commands
{
    //this is for commands for edit mode
    public interface IPlacementCommand
    {
        void Execute(IPlacementActor actor);
    }
}