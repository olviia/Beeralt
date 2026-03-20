using System.IO;
using UnityEngine;

namespace Helpers
{
    public static class SavePaths
    {
        public static string leaderboard => Combine("leaderboard.json");
        public static string savedObjects => Combine( "savedObjects.json");

        private static string Combine(string add)
        {
            return Path.Combine(Application.persistentDataPath, add);
        }
        
    }
}