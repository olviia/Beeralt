using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Timer:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeText;
        private float elapsedTime;
        string currentTime;

        private void Update()
        {
            elapsedTime += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
            currentTime = $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}.{timeSpan.Milliseconds:000}";
            timeText.text = currentTime;
        }

        public float GetTime()
        {
            return elapsedTime;
        }

        public string GetFormattedTime()
        {
            return currentTime;
        }
    }
}