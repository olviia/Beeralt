using Actor;

namespace Commands
{
    public interface IActionCommand
    {
        void Execute(IGameActor actor);
    }
}