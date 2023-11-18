using System;
using gameoff.Enemy.Projectiles;
using UnityEngine;

namespace gameoff.World
{
    public class HumanBase : MonoBehaviour
    {
        public static event Action CreepReachedBase;
        private bool _isCreepReached;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isCreepReached)
                return;

            if (other.TryGetComponent(out HiveProjectile hiveProjectile))
            {
                CreepReachedBase?.Invoke();
                _isCreepReached = true;
            }
        }
    }
}