namespace Actor
{
    //game actor is basically a player, somebody who acts and does something
    //menu actor is something that controls the state of the game and ui
    public interface IMenuActor
    {
        void Exit();
        void ToggleEditMenu();
        void RightClickMenu();
    }
}