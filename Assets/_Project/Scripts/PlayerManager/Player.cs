using System;
using gameoff.Enemy;
using UnityEngine;

namespace gameoff.PlayerManager
{
    public class Player : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public int StartHealth { private set; get; } = 20;

        public static Player Current { get; private set; }
        public event Action Died;
        public int CurrentHealth { get; private set; }

        private void Awake()
        {
            Current = this;
            CurrentHealth = StartHealth;
        }

        public void TakeDamage(int count)
        {
            CurrentHealth -= count;

            if (CurrentHealth <= 0)
                Die();
            
            Debug.Log("Damage taken");
        }

        private void Die()
        {
            Died?.Invoke();
            gameObject.SetActive(false);
        }
    }
}