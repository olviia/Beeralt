using System;
using System.IO;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Leaderboard
{
    public class LeaderboardController:MonoBehaviour
    {
        public LeaderboardEntityPrefab EntryPrefab;
        public LeaderboardEntityPrefab newEntryPrefab;
        public GameObject parent;

        private void Start()
        {
            string path = SavePaths.leaderboard;

            // LeaderboardData data = JsonUtility.FromJson<LeaderboardData>(File.ReadAllText(path));
            LeaderboardData data = SaveSystem.Load<LeaderboardData>(path);
            
            
            foreach (LeaderboardEntry entry in data.entries)
            {
                
                if (entry.isLatest)
                {
                    var prefab = Instantiate(newEntryPrefab, parent.transform);
                    prefab.SetText(entry.playerName, entry.timeFormatted);
                }
                else
                {
                    var prefab = Instantiate(EntryPrefab, parent.transform);
                    prefab.SetText(entry.playerName, entry.timeFormatted);
                }
            }
        }

        public void RestartGame(){
            Time.timeScale = 1;
            SceneManager.LoadScene("bee");
        }
    }
}