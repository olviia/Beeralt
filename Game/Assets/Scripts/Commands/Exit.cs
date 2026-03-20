using Actor;

namespace Commands
{
    public class Exit: IMetaCommand
    {
        public void Execute(IMenuActor actor)
        {
            actor.Exit();
        }
    }
}