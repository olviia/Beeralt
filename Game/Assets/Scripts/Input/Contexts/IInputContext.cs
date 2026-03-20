namespace Input.Contexts
{
    public interface IInputContext
    {
        bool IsMovementActive { get; }
        bool IsActionActive { get; }
        bool IsPlacementActive { get; }
        
        //meta is for menus and other actions from play mode
        bool IsMetaActive { get; }
        //and this is for navigating in menus
        bool IsNavigatableMenuActive { get; }
    }
}