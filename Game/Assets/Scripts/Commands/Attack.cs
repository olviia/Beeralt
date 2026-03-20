using Actor;

namespace Commands
{
    public class Attack:IActionCommand
    {
        public void Execute(IGameActor actor)
        {
            actor.Attack();        }
    }
}