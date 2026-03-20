using Actor;

namespace Commands
{
    public class MoveBackward:IMovementCommand
    {
        public void Execute(IGameActor actor)
        {
            actor.MoveBackward();
        }
    }
}