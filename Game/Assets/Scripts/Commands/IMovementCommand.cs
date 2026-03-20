using Actor;

namespace Commands
{
    //Command design pattern
    //for user input
    //two layers of abstraction
    //this one is for player actions
    public interface IMovementCommand
    {
        //actors here is who implement the command that is executed
        void Execute(IGameActor actor);

    }
}