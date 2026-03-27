using System;
using Actor;
using UnityEngine;

namespace PhysicsElement
{
    public class Projectile:MonoBehaviour
    {
        public int damage;
        
        //we should have collider that is not trigger here

        private void OnCollisionEnter(Collision other)
        {
            var hittable = other.gameObject.GetComponent<IHittable>();
            if (hittable != null)
            {
                hittable.TakeHit(damage);
            }

            Destroy(gameObject);
        }
    }
}