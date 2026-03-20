namespace Input.Contexts
{
    public class EditContext:IInputContext
    {
        public bool IsMovementActive => true;
        public bool IsActionActive => false;
        public bool IsPlacementActive => true;
        public bool IsMetaActive => true;
        public bool IsNavigatableMenuActive => false;
    }
}