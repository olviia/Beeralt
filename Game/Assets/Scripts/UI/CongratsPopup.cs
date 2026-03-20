using System;
using System.IO;
using Helpers;
using Leaderboard;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace UI
{
    public class CongratsPopup:PausingMenu
    {
        //go straight to leaderboard
        public Timer timer;
        public TMP_InputField inputField;

        public void Continue()
        {
            
            //reading and writing everything to json
            LeaderboardData data;

            string path = SavePaths.leaderboard;
            data = SaveSystem.Load<LeaderboardData>(path);

            foreach (LeaderboardEntry entry in data.entries)
            {
                entry.isLatest = false;
            }

            LeaderboardEntry newEntry = new LeaderboardEntry
            {
                playerName = inputField.text,
                isLatest = true,
                time = timer.GetTime(),
                timeFormatted = timer.GetFormattedTime()
            };
            
            data.entries.Add(newEntry);
            
            data.entries.Sort((x, y) => x.time.CompareTo(y.time));
            
            SaveSystem.Save<LeaderboardData>(path, data);
            
            
            SceneManager.LoadScene("Leaderboard");
        }
        
        public override void Open()
        {
            base.Open();
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            base.Close();
            gameObject.SetActive(false);
        }

    }
}