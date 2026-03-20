using System;

namespace Locations
{
    
    //this is for somebody who can visit different locations
    //mainly player
    public interface ILocationVisitor
    {
        public event Action<LocationData> OnLocationVisited;
        public event Action<LocationData> OnLocationLeft;

        
        //gives what location was visited
        public void LocationVisited(LocationData location);
        
        //and location that was left
        public void LocationLeft(LocationData location);
    }
}