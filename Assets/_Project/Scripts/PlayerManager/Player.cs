using System;
using gameoff.Enemy;
using UnityEngine;

namespace gameoff.PlayerManager
{
    public class Player : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public int StartHealth { private set; get; } = 20;

        public SpriteRenderer SpriteRenderer { private set; get; }
        public static Player Current { get; private set; }
        
        public static event Action Died;
        public static event Action<int> HealthChanged;
        public int CurrentHealth { get; private set; }

        private bool _isAlive = true;

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Current = this;
            CurrentHealth = StartHealth;
        }

        public void TakeDamage(int count)
        {
            if (!_isAlive)
                return;

            CurrentHealth -= count;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Die();
            }

            HealthChanged?.Invoke(CurrentHealth);

            Debug.Log("Damage taken");
        }

        private void Die()
        {
            _isAlive = false;
            Died?.Invoke();
            gameObject.SetActive(false);
        }
    }
}