namespace Input.Contexts
{
    //for those menus that can be navigated
    public class MenuContext:IInputContext
    {
        public bool IsMovementActive => false;
        public bool IsActionActive =>false;
        public bool IsPlacementActive =>false;
        public bool IsMetaActive => true;
        public bool IsNavigatableMenuActive => true;
    }
}