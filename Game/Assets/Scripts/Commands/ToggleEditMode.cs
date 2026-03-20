using Actor;

namespace Commands
{
    public class ToggleEditMode:IMetaCommand
    {
        public void Execute(IMenuActor actor)
        {
            actor.ToggleEditMenu();
        }
    }
}