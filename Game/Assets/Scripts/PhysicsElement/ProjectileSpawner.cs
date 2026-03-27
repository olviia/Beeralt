using UnityEngine;

namespace PhysicsElement
{
    public class ProjectileSpawner:MonoBehaviour
    {
        //put here prefab of projectile
        public Rigidbody projectile;
        public float projectileForce;

        public void SpawnProjectile()
        {
            var newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            newProjectile.transform.parent = transform;
            
            newProjectile.AddForce(transform.forward * projectileForce, ForceMode.Impulse);
        }
    }
}