namespace Actor
{
    public interface IPlacementActor
    {
        void PlaceMove();
        void Scroll(int direction);
        void Inspect();

    }
}