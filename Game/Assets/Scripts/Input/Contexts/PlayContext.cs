namespace Input.Contexts
{
    public class PlayContext:IInputContext
    {
        public bool IsMovementActive => true;
        public bool IsActionActive => true;
        public bool IsPlacementActive => false;
        public bool IsMetaActive => true;
        public bool IsNavigatableMenuActive => false;
    }
}