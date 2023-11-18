using System;
using gameoff.World;
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

        [Header("Creep Parameters")]
        [SerializeField] private int fillCreepRadius = 50;

        [Inject] private DiContainer _diContainer;
        [Inject] private ICreepClearing _creepClearing;
        
        public static event Action<Hive> Died;
        public int CurrentHealth { get; private set; } = 100;

        private HiveEnemyFactory _hiveEnemyFactory;

        private void Awake()
        {
            CurrentHealth = StartHealth;            
            _hiveEnemyFactory = new HiveEnemyFactory(_diContainer, this, spawnOuterRadius, spawnInnerRadius,
                spawnDelayInSeconds, enemySpawnSettings);
        }

        private void Start()
        {
            _hiveEnemyFactory.StartSpawning();
            _creepClearing.AddCreep(transform.position, fillCreepRadius);
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