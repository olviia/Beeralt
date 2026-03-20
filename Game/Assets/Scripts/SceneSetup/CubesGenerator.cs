using UnityEngine;

namespace SceneSetup
{
    public class CubesGenerator : MonoBehaviour
    {
        public int width; //x
        public int length; //z
        public int number;
        public int cubesHeight;

        void Start()
        {
            Generate();
        }

        [ContextMenu("CallGenerate")]
        void Generate()
        {
            for (int i = 0; i < number; i++)
            {
                // Create a cube primitive
                GameObject mesh = GameObject.CreatePrimitive(PrimitiveType.Cube);

                mesh.transform.SetParent(this.transform);

                // Set position and scale
                mesh.transform.localScale = new Vector3(Random.Range(0.5f, 3f), Random.Range(0.5f, cubesHeight), Random.Range(0.5f, 3f));

                mesh.transform.position = new Vector3(Random.Range(-width, width), (float)(mesh.transform.localScale.y / 2), Random.Range(-length, length));




            }

        }
    }
}
