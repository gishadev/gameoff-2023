using gameoff.Core;
using UnityEngine;

namespace gameoff.Enemy.Projectiles
{
    public class EnemyProjectile : Projectile
    {
        private int _damageCount = 1;

        public void SetDamage(int damageCount)
        {
            _damageCount = damageCount;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if (other.TryGetComponent(out IDamageable damageable))
                if (damageable is not Hive)
                    damageable.TakeDamage(_damageCount);

            Die();
        }
    }
}