using Actor;

namespace Commands
{
    public class Scroll:IPlacementCommand
    {
        private int direction;
        public Scroll(float direction)
        {
            //it can be only 1 and -1 but originally is passed as a float
            //so i cast it to int here
            this.direction = (int)direction;
        }
        public void Execute(IPlacementActor actor)
        {
            actor.Scroll(direction);
        }
    }
}
