using Actor;

namespace Commands
{
    public class Interact:IActionCommand
    {
        public void Execute(IGameActor actor)
        {
            actor.Interact();
        }
    }
}