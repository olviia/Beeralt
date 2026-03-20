using Actor;

namespace Commands
{
    public class MoveLeft:IMovementCommand
    {
        public void Execute(IGameActor actor)
        {
            actor.MoveLeft();
        }

    }
}