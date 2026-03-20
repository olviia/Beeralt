using Actor;
using UnityEngine;

namespace Commands
{
    public class MoveForward: IMovementCommand
    {
        
        public void Execute(IGameActor actor)
        {
            actor.MoveForward();
        }

    }
}