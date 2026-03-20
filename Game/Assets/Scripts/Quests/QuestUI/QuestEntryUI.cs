using TMPro;
using UnityEngine;

namespace Quests.QuestUI
{
    //for the prefab that holds quest
    public class QuestEntryUI:MonoBehaviour
    {
        public TextMeshProUGUI title;

        public void SetText(string text)
        {
            title.text = text;
        }

        public void MarkComplete()
        {
            title.fontStyle = FontStyles.Strikethrough;
            title.color = Color.gray;
        }
    }
}