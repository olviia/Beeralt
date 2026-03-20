using Actor;

namespace Commands
{
    public class PlaceMove:IPlacementCommand
    {
        public void Execute(IPlacementActor actor)
        {
            actor.PlaceMove();
        }
    }
}