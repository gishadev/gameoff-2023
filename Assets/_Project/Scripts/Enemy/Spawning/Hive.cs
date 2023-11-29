using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using gameoff.Core;
using gameoff.World;
using gishadev.tools.Effects;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace gameoff.Enemy
{
    public class Hive : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public int StartHealth { get; private set; } = 100;

        [Header("Spawning Parameters")]
        [SerializeField]
        private float spawnDelayInSeconds = 3f;

        [SerializeField] private float spawnOuterRadius = 2f, spawnInnerRadius = 1f;

        [SerializeField, TableList] private EnemySpawnSettings[] enemySpawnSettings;

        [BoxGroup("Creep expanding")]
        [SerializeField]
        private int spawnFillCreepRadius = 50;

        [BoxGroup("Creep expanding")]
        [SerializeField]
        private float creepGrowDelay = 5f;

        
        [BoxGroup("Clearing on die")]
        [SerializeField]
        private int dieClearCreepRadius = 150;

        [BoxGroup("Clearing on die")]
        [SerializeField]
        private float dieClearCreepExpandingTime = 5f;

        [BoxGroup("Clearing on die")]
        [SerializeField]
        private int dieClearCreepIterationsCount = 25;

        [Inject] private DiContainer _diContainer;
        [Inject] private ICreepClearing _creepClearing;

        public static event Action<Hive> Died;
        public int CurrentHealth { get; private set; } = 100;
        public event Action<int> HealthChanged;

        private HiveEnemyFactory _hiveEnemyFactory;
        private HumanBase _humanBase;
        private CancellationTokenSource _creepGrowCTS;

        private void Awake()
        {
            CurrentHealth = StartHealth;
            _hiveEnemyFactory = new HiveEnemyFactory(_diContainer, this, spawnOuterRadius, spawnInnerRadius,
                spawnDelayInSeconds, enemySpawnSettings);
        }

        private void Start()
        {
            _creepGrowCTS = new CancellationTokenSource();
            _creepGrowCTS.RegisterRaiseCancelOnDestroy(this);

            _hiveEnemyFactory.StartSpawning();

            _creepClearing.AddCreep(transform.position, spawnFillCreepRadius, out var pixelsChanged);
            _humanBase = FindObjectOfType<HumanBase>();
            GrowCreepAsync();
        }

        public void TakeDamage(int count)
        {
            CurrentHealth -= count;

            if (CurrentHealth <= 0)
                Die();

            HealthChanged?.Invoke(CurrentHealth);
        }

        private void Die()
        {
            ClearAreaInCircleAsync(dieClearCreepIterationsCount, dieClearCreepExpandingTime);

            Destroy(gameObject);
            Died?.Invoke(this);
        }

        private async void ClearAreaInCircleAsync(int clearIterations, float expandingTime)
        {
            var creepClearing = _creepClearing;
            var position = transform.position;

            var rStep = dieClearCreepRadius / clearIterations;
            var clearRadius = rStep;
            var iterationTime = expandingTime / clearIterations;
            for (int i = 0; i < clearIterations; i++)
            {
                creepClearing.ClearCreep(position, Mathf.RoundToInt(clearRadius));
                await UniTask.WaitForSeconds(iterationTime);
                clearRadius += rStep;
            }
        }

        private async void GrowCreepAsync()
        {
            while (!_creepGrowCTS.IsCancellationRequested)
            {
                var dirToHumanBase = (_humanBase.transform.position - transform.position).normalized;
                float rotZ = Mathf.Atan2(dirToHumanBase.y, dirToHumanBase.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(rotZ, Vector3.forward);

                var proj = OtherEmitter.I.EmitAt(OtherPoolEnum.HIVE_PROJECTILE, transform.position, rotation);
                _diContainer.InjectGameObject(proj);

                await UniTask.WaitForSeconds(creepGrowDelay, cancellationToken: _creepGrowCTS.Token)
                    .SuppressCancellationThrow();
            }
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