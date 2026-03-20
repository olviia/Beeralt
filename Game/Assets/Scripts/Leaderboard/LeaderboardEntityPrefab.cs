using TMPro;
using UnityEngine;

namespace Leaderboard
{
    public class LeaderboardEntityPrefab:MonoBehaviour
    {
        public TMP_Text playerName;
        public TMP_Text time;

        public void SetText(string _name, string _time)
        {
            playerName.text = _name;
            time.text = _time;
        }
        
        
    }
}