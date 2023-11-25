using System;
using gameoff.Enemy.SOs;
using gishadev.tools.StateMachine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Player = gameoff.PlayerManager.Player;

namespace gameoff.Enemy
{
    [RequireComponent(typeof(EnemyMovement))]
    public abstract class Enemy : MonoBehaviour, IDamageable
    {
        public abstract EnemyDataSO EnemyDataSO { get; }

        public event Action<int> HealthChanged;
        public int StartHealth => EnemyDataSO.StartHealth;
        public IEnemySpawnData SpawnData { private set; get; }
        protected StateMachine StateMachine;
        public int CurrentHealth { get; private set; }
        protected EnemyMovement EnemyMovement { get; private set; }

        public Vector2 StartPosition { get; private set; }

        private void Awake()
        {
            EnemyMovement = GetComponent<EnemyMovement>();
        }

        private void OnEnable()
        {
            CurrentHealth = EnemyDataSO.StartHealth;
            InitStateMachine();
        }

        private void Start() => StartPosition = transform.position;

        private void Update() => StateMachine.Tick();

        protected abstract void InitStateMachine();

        public virtual void TakeDamage(int count)
        {
            CurrentHealth -= count;
            HealthChanged?.Invoke(CurrentHealth);
        }

        public void SetSpawnData(IEnemySpawnData spawnData) => SpawnData = spawnData;

        protected bool IsInStartArea() =>
            Vector3.Distance(transform.position, StartPosition) < EnemyDataSO.StartAreaRadius;

        protected bool InSightWithPlayer() =>
            Vector3.Distance(Player.Current.transform.position, transform.position) < EnemyDataSO.FollowRadius;

        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, EnemyDataSO.MeleeAttackRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(StartPosition, EnemyDataSO.StartAreaRadius);
        }
    }
}