using Actor;

namespace Commands
{
    public class MoveDown: IMovementCommand
    {
        public void Execute(IGameActor actor)
        {
            actor.MoveDown();
        }
    }
}