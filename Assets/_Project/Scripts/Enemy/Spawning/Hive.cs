using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using gameoff.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace gameoff.Enemy
{
    public class Hive : MonoBehaviour, IDamageable
    {
        [SerializeField] private float spawnRadius = 1f;
        [SerializeField] private float spawnDelayInSeconds = 3f;

        [SerializeField] private EnemySpawnSettings[] enemySpawnSettings;

        public static event Action<Hive> Died;
        public int CurrentHealth { get; private set; } = 100;

        private bool _isSpawning;

        private void Start()
        {
            _isSpawning = true;
            SpawningAsync();
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

        private async void SpawningAsync()
        {
            while (_isSpawning)
            {
                // Get enemies settings, where spawned enemies count less than max count, available to be spawned.
                var availableEnemies = enemySpawnSettings
                    .Where(x => GetSpawnedEnemies(x).Length < x.MaxCount)
                    .ToArray();

                // Choose random enemy spawn settings using compound choosing system.
                var compoundChance = availableEnemies.Sum(x => x.SpawnChance);
                var rValue = Random.Range(0f, compoundChance);

                float leftChanceBound = 0, rightChanceBound = 0;
                EnemySpawnSettings randomEnemySpawnSettings = null;
                for (int i = 0; i < availableEnemies.Length; i++)
                {
                    rightChanceBound = leftChanceBound + availableEnemies[i].SpawnChance;

                    if (rValue > leftChanceBound && rValue < rightChanceBound)
                    {
                        randomEnemySpawnSettings = availableEnemies[i];
                        break;
                    }

                    leftChanceBound = rightChanceBound;
                }

                if (randomEnemySpawnSettings != null)
                {
                    // Choose random count to spawn.
                    var randomCountToSpawn = Random.Range(randomEnemySpawnSettings.MinSpawnCount,
                        randomEnemySpawnSettings.MaxSpawnCount);
                    int diffToMaxCount = randomEnemySpawnSettings.MaxCount -
                                         GetSpawnedEnemies(randomEnemySpawnSettings).Length;
                    var countToSpawn = randomCountToSpawn > diffToMaxCount ? diffToMaxCount : randomCountToSpawn;

                    // Actual spawning.
                    for (int i = 0; i < countToSpawn; i++)
                    {
                        var spawnedEnemy = Instantiate(randomEnemySpawnSettings.EnemyPrefab, transform.position,
                            Quaternion.identity).GetComponent<Roach>();
                        spawnedEnemy.SetSpawnData(new EnemySpawnData(this,
                            randomEnemySpawnSettings.EnemyPrefab.GetInstanceID().ToString()));
                    }
                }

                await UniTask.WaitForSeconds(spawnDelayInSeconds);
            }
        }

        private Roach[] GetSpawnedEnemies(EnemySpawnSettings enemySettings)
        {
            return FindObjectsOfType<Roach>().Where(x =>
                    x.SpawnData != null &&
                    x.SpawnData.HiveOrigin == this &&
                    x.SpawnData.PrefabID == enemySettings.EnemyPrefab.GetInstanceID().ToString())
                .ToArray();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
    }
}