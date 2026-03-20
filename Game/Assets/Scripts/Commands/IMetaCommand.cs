using Actor;

namespace Commands
{
    //this is for all the commands for menu from play mode
    public interface IMetaCommand
    {
        void Execute(IMenuActor actor);
    }
}