using Actor;

namespace Commands
{
    public class Inspect:IPlacementCommand
    {
        public void Execute(IPlacementActor actor)
        {
            actor.Inspect();
        }
    }
}