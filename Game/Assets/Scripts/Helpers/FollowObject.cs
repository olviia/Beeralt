using UnityEngine;

namespace Helpers
{
    public class FollowObject : MonoBehaviour
    {
        
        [SerializeField] private GameObject objectToFollow;
        [SerializeField] private GameObject objectThatFollows;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            objectThatFollows.transform.position = objectToFollow.transform.position;
        }
    }
}
