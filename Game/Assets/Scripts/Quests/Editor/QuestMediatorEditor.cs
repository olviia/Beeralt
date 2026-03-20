using Actor;
using UnityEditor;
using UnityEngine;

namespace Quests.Editor

{
    //i did this editor because i want to put here a game actor, but i can't put 
    //an interface as a serializable object, so i have to create this way around
    [CustomEditor(typeof(QuestMediator))]
    public class QuestMediatorEditor:UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            var player = serializedObject.FindProperty("player");
            if (player.objectReferenceValue != null &&
                (((GameObject)(player.objectReferenceValue)).GetComponent<IGameActor>() == null))
            {
                EditorGUILayout.HelpBox("This script can only work with the player that has a GameActor component.",
                    MessageType.Error);
            }
        }
    }
}