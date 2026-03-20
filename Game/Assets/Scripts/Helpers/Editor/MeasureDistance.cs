using UnityEditor;
using UnityEngine;

namespace Helpers.Editor
{
    public class MeasureDistance:EditorWindow
    {
        private GameObject objectA;
        private GameObject objectB;
        private float distance;

        [MenuItem("Window/Helpers/MeasureDistance")]
        public static void Open()
        {
            GetWindow<MeasureDistance>("MeasureDistance");
        }

        private void OnGUI()
        {
            objectA = EditorGUILayout.ObjectField("Object A", objectA, typeof(GameObject), true) as GameObject;
            objectB = EditorGUILayout.ObjectField("Object B", objectB, typeof(GameObject), true) as GameObject;
            if (GUILayout.Button("Measure") && objectA != null && objectB != null)
            {
                distance = Vector3.Distance(objectA.transform.position, objectB.transform.position);
            }
            EditorGUILayout.LabelField($"Distance",distance.ToString("F4") );

        }

    }
}