using System;
using gameoff.Core;
using gameoff.Enemy;
using UnityEngine;

namespace gameoff.PlayerManager
{
    public class Player : MonoBehaviour, IDamageableWithPhysicsImpact
    {
        [field: SerializeField] public int StartHealth { private set; get; } = 20;

        public SpriteRenderer SpriteRenderer { private set; get; }
        public static Player Current { get; private set; }
        public PhysicsImpactEffector PhysicsImpactEffector { get; private set; }

        public static event Action Died;
        public event Action<int> HealthChanged;
        public int CurrentHealth { get; private set; }

        private bool _isAlive = true;

        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Current = this;
        }

        private void Start()
        {
            PhysicsImpactEffector = new PhysicsImpactEffector(_playerMovement.Rigidbody, _playerMovement);
        }

        private void OnEnable()
        {
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