using TMPro;
using UnityEngine;

namespace Quests.QuestUI
{
    //for the prefab that holds objectives
    public class ObjectiveEntryUI:MonoBehaviour
    {
        public TextMeshProUGUI text;
        private Color originalColor;

        void Awake()
        {
            originalColor = text.color;
        }

        public void SetText(string text)
        {
            this.text.text = text;
        }

        public void MarkComplete()
        {
            text.fontStyle = FontStyles.Strikethrough;
            text.color = Color.gray;
        }
        public void MarkNotComplete()
        {
            text.fontStyle = FontStyles.Normal;
            text.color = originalColor;
        }
        
    }
}