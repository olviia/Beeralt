using UnityEngine;
using Locations;
namespace Quests
{    
    [CreateAssetMenu(fileName = "GoToObjective", menuName = "ScriptableObjects/Quest/GoTo")]

    public class LocationObjective: QuestObjective
    {
        [SerializeField] private LocationData locationData;

        public override bool? CheckLocationReached(LocationData location)
        {
            if (locationData == location) return true;
            return null;
        }
        
        
        //todo:
        //here get the distance between player and target location and display it 
        public override string GetDescription()
        {
            return "Go to " + locationData.LocationName;
        }
    }
}