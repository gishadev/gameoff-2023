using System.Linq;
using Cysharp.Threading.Tasks;
using gishadev.tools.Effects;
using UnityEngine;
using Zenject;

namespace gameoff.Enemy
{
    public class EnemyFactory
    {
        private readonly DiContainer _diContainer;
        private readonly Hive _hiveOrigin;
        private readonly float _spawnOuterRadius;
        private readonly float _spawnInnerRadius;
        private readonly float _spawnDelayInSeconds;
        private readonly EnemySpawnSettings[] _enemySpawnSettings;
        private bool _isSpawning;

        private readonly Transform _parent;

        public EnemyFactory(DiContainer diContainer, Hive hiveOrigin, float spawnOuterRadius, float spawnInnerRadius,
            float spawnDelayInSeconds,
            EnemySpawnSettings[] enemySpawnSettings)
        {
            _diContainer = diContainer;
            _hiveOrigin = hiveOrigin;
            _spawnOuterRadius = spawnOuterRadius;
            _spawnInnerRadius = spawnInnerRadius;
            _spawnDelayInSeconds = spawnDelayInSeconds;
            _enemySpawnSettings = enemySpawnSettings;

            _parent = new GameObject("[Enemy Factory]").transform;
        }


        public void StartSpawning()
        {
            if (!_isSpawning)
            {
                _isSpawning = true;
                SpawningAsync();
            }
        }

        public void StopSpawning()
        {
            _isSpawning = false;
        }

        private async void SpawningAsync()
        {
            while (_isSpawning && _hiveOrigin != null)
            {
                var randomEnemySpawnSettings = GetRandomEnemySettings(GetAvailableEnemiesToSpawn());
                if (randomEnemySpawnSettings != null)
                {
                    var countToSpawn = GetCountToSpawn(randomEnemySpawnSettings);
                    for (int i = 0; i < countToSpawn; i++)
                    {
                        var rPosition = (Vector2) _hiveOrigin.transform.position + GetRandomCircularPosition();
                        SpawnEnemy(randomEnemySpawnSettings, rPosition);
                    }
                }

                await UniTask.WaitForSeconds(_spawnDelayInSeconds);
            }
        }

        private Vector2 GetRandomCircularPosition()
        {
            float angle = Random.Range(0f, 360f);

            float radius = Random.Range(_spawnInnerRadius, _spawnOuterRadius);

            float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            return new Vector2(x, y);
        }

        private void SpawnEnemy(EnemySpawnSettings randomEnemySpawnSettings, Vector2 position)
        {
            var spawnedEnemy =
                OtherEmitter.I.EmitAt(OtherPoolEnum.ROACH, position, Quaternion.identity)
                    .GetComponent<Roach>();
            spawnedEnemy.transform.SetParent(_parent);

            spawnedEnemy.SetSpawnData(new EnemySpawnData(_hiveOrigin,
                randomEnemySpawnSettings.EnemyPrefab.GetInstanceID().ToString()));
        }

        /// <summary>
        /// Choose random count to spawn.
        /// </summary>
        private int GetCountToSpawn(EnemySpawnSettings randomEnemySpawnSettings)
        {
            var randomCountToSpawn = Random.Range(randomEnemySpawnSettings.MinSpawnCount,
                randomEnemySpawnSettings.MaxSpawnCount);
            int diffToMaxCount = randomEnemySpawnSettings.MaxCount -
                                 GetSpawnedEnemies(randomEnemySpawnSettings).Length;
            var countToSpawn = randomCountToSpawn > diffToMaxCount ? diffToMaxCount : randomCountToSpawn;
            return countToSpawn;
        }

        /// <summary>
        /// Choose random enemy spawn settings using compound choosing system.
        /// </summary>
        private EnemySpawnSettings GetRandomEnemySettings(EnemySpawnSettings[] availableEnemies)
        {
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

            return randomEnemySpawnSettings;
        }

        private EnemySpawnSettings[] GetAvailableEnemiesToSpawn()
        {
            // Get enemies settings, where spawned enemies count less than max count, available to be spawned.
            var availableEnemies = _enemySpawnSettings
                .Where(x => GetSpawnedEnemies(x).Length < x.MaxCount)
                .ToArray();
            return availableEnemies;
        }

        private Roach[] GetSpawnedEnemies(EnemySpawnSettings enemySettings)
        {
            return Object.FindObjectsOfType<Roach>().Where(x =>
                    x.SpawnData != null &&
                    x.SpawnData.HiveOrigin == _hiveOrigin &&
                    x.SpawnData.PrefabID == enemySettings.EnemyPrefab.GetInstanceID().ToString())
                .ToArray();
        }
    }
}