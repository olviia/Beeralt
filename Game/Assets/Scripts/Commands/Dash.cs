using Actor;

namespace Commands
{
    public class Dash:IActionCommand
    {
        public void Execute(IGameActor actor)
        {
            actor.Dash();
        }
    }
}