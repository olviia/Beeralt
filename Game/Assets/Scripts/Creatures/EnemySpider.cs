using System;
using Actor;
using Attributes;
using UnityEngine;

namespace Creatures
{
    public class EnemySpider:MonoBehaviour, IHittable
    {
        public Health health { get; private set; }
        public Rigidbody target;
        public int gazeDistance = 5;
        public float cooldown = 4f;
        
        private float cooldownTimer;
        private Animator animator;
        public void TakeHit(int damage)
        {
            health.Decrease(damage);
        }
        

        private void Update()
        {
            if ((target.transform.position - transform.position).magnitude < gazeDistance)
            {
                transform.LookAt(target.transform);
                cooldownTimer -= Time.deltaTime;

                if (cooldownTimer <= 0f)
                {
                    animator.SetTrigger("Attack");
                    cooldownTimer = cooldown;
                }
            }
        }

    }
}