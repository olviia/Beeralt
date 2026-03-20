using UnityEngine;

namespace Helpers
{
    public static class ShakeDetector
    {
        //how fast mouse should move so we consider it shaking
        private static float minMouseSpeed = 5f;
        //how many times we should change direction to consider it shaking
        private static int directionChangesRequired = 4;
    
        //time in seconds between shakes
        private static float accumulationTime = 0.2f;
        private static int currentDirectionChanges = 0;

        private static float timer = 0f;
        private static Vector2 lastDelta;
        private static int lastDirX;
        private static int lastDirY;
        public static bool IsShaking(Vector2 input)
        {
            timer +=Time.deltaTime;
            //reset timer when there was no movement for too long
            if(timer > accumulationTime)
            {
                currentDirectionChanges = 0;
                timer = 0f;
            }
        
            //ignore tiny movements;
            if(input.magnitude < minMouseSpeed*Time.deltaTime)
                return false;

            //get the sign of direction
            int dirX = input.x == 0? 0: ((int)(input.x/Mathf.Abs(input.x)));
            int dirY = input.y == 0? 0: ((int)(input.y/Mathf.Abs(input.y)));

//for now only detect the shake around x
            if((dirX != 0 && dirX != lastDirX))
            {
                currentDirectionChanges++;
                timer = 0f;
            }

            //assignment for the future
            if(dirX!=0)lastDirX = dirX;
            if(dirY!=0)lastDirY = dirY;
        
            if(currentDirectionChanges >= directionChangesRequired)
            {
                currentDirectionChanges = 0;
                timer = 0;
                return true;

            }
        
            return false;
        
        }
    }
}