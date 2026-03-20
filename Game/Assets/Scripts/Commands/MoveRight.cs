using Actor;

namespace Commands
{
    public class MoveRight:IMovementCommand
    {
        public void Execute(IGameActor actor)
        {
            actor.MoveRight();
        }

    }
}