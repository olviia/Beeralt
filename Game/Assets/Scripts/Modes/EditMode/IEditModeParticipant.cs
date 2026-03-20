namespace Modes.EditMode
{
    //so every element in edit mode can be turned on from edit mode controller
    //so we can activate and deactivate all of them from one place
    public interface IEditModeParticipant
    {
        void OnEnterEditMode();
        void OnExitEditMode();
        
    }
}