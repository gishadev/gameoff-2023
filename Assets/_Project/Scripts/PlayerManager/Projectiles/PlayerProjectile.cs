﻿using gameoff.Core;
using gameoff.Enemy;
using UnityEngine;

namespace gameoff.PlayerManager
{
    public class PlayerProjectile : Projectile
    {
        private int _damageCount = 1;

        public void SetDamage(int damageCount)
        {
            _damageCount = damageCount;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_damageCount);

            Die();
        }
    }
}