using System;
using gameoff.Enemy;
using UnityEngine;

namespace gameoff.World
{
    public class Hive : MonoBehaviour, IDamageable
    {
        public static event Action<Hive> Died;
        public int CurrentHealth { get; private set; } = 100;


        public void TakeDamage(int count)
        {
            CurrentHealth -= count;

            if (CurrentHealth <= 0)
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);
            
            Died?.Invoke(this);
        }
        
        private void OnParticleCollision(GameObject other)
        {
            TakeDamage(1);
        }
    }
}