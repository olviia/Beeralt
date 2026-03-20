using Actor;

namespace Commands
{
    public class MoveUp:IMovementCommand
    {
        public void Execute(IGameActor actor)
        {
            actor.MoveUp();
        }
    }
}