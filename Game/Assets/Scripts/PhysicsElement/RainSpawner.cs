using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PhysicsElement
{
    public class RainSpawner:MonoBehaviour
    {
        [SerializeField] private GameObject spawnShape;
        [SerializeField] private RainDrop dropPrefab;
        [SerializeField] private float spawnSpeed = 5f;
        [SerializeField] private float dropSpeed = 5f;

        private Bounds bounds;
        

        private void Start()
        {
            //very interesting thing instead of coroutine
            InvokeRepeating("SpawnDrop", 0, spawnSpeed);
            bounds = spawnShape.GetComponent<Renderer>().localBounds;
        }

        private void SpawnDrop()
        {

            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = bounds.center.y;
            float z = Random.Range(bounds.min.z, bounds.max.z);
            
            //move from local to world
            Vector3 spawnLocalPosition = new Vector3(x, y, z);
            Vector3 spawnWorldPosition = spawnShape.transform.TransformPoint(spawnLocalPosition);
            
            var newDrop = Instantiate(dropPrefab, spawnWorldPosition, Quaternion.identity);
            var rigidBody = newDrop.GetComponent<Rigidbody>();
            rigidBody.useGravity = false;
            rigidBody.linearVelocity = -spawnShape.transform.up * dropSpeed;

        }
    }

}