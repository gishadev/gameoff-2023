using System;
using UnityEngine;
using Zenject;

namespace gameoff.Enemy
{
    public class Hive : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public int StartHealth { get; private set; } = 100;

        [Header("Spawning Parameters")]
        [SerializeField] private float spawnDelayInSeconds = 3f;
        [SerializeField] private float spawnOuterRadius = 2f, spawnInnerRadius = 1f;

        [SerializeField] private EnemySpawnSettings[] enemySpawnSettings;

        [Inject] private DiContainer _diContainer;

        public static event Action<Hive> Died;
        public int CurrentHealth { get; private set; } = 100;

        private EnemyFactory _enemyFactory;

        private void Awake()
        {
            CurrentHealth = StartHealth;            
            _enemyFactory = new EnemyFactory(_diContainer, this, spawnOuterRadius, spawnInnerRadius,
                spawnDelayInSeconds, enemySpawnSettings);
        }

        private void Start()
        {
            _enemyFactory.StartSpawning();
        }


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


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, spawnInnerRadius);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, spawnOuterRadius);
        }
    }
}