using System;

namespace Leaderboard
{
    [Serializable]
    public class LeaderboardEntry
    {
        public string playerName;
        public float time;
        public string timeFormatted;
        public bool isLatest;
    }
}