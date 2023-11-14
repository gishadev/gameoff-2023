using System;
using Cysharp.Threading.Tasks;
using gameoff.Enemy;
using UnityEngine;
using Zenject;

namespace gameoff.PlayerManager
{
    public class Projectile : MonoBehaviour
    {
        [field: SerializeField] protected float FlySpeed { private set; get; } = 30f;
        [field: SerializeField] protected float LifeTime { private set; get; } = 0.5f;
        
        private int _damageCount = 1;
        
        protected virtual void OnEnable()
        {
            LifetimeAsync();
        }

        public void SetDamage(int damageCount)
        {
            _damageCount = damageCount;
        }
        
        protected virtual void Update()
        {
            transform.Translate(transform.right * (FlySpeed * Time.deltaTime), Space.World);
        }
        
        protected void Die()
        {
            gameObject.SetActive(false);
        }

        private async void LifetimeAsync()
        {
            await UniTask.WaitForSeconds(LifeTime);
            Die();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_damageCount);

            Die();
        }

    }
}