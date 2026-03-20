using System;
using System.Collections.Generic;

namespace Leaderboard
{
    [Serializable]
    public class LeaderboardData
    {
        public List<LeaderboardEntry> entries = new();
    }
}