using System;
using gameoff.Core;
using gameoff.Enemy.SOs;
using gameoff.UI.Game;
using gishadev.tools.Audio;
using gishadev.tools.StateMachine;
using UnityEngine;
using Zenject;
using Player = gameoff.PlayerManager.Player;

namespace gameoff.Enemy
{
    [RequireComponent(typeof(EnemyMovement), typeof(EnemyAnimationsHandler))]
    public abstract class Enemy : MonoBehaviour, IDamageableWithPhysicsImpact
    {
        [Inject] protected DiContainer DiContainer;

        public abstract EnemyDataSO EnemyDataSO { get; protected set; }

        public event Action<int> HealthChanged;
        public int StartHealth => EnemyDataSO.StartHealth;
        public IEnemySpawnData SpawnData { private set; get; }
        protected StateMachine StateMachine;
        public int CurrentHealth { get; private set; }
        public EnemyAnimationsHandler AnimationsHandler { get; private set; }
        protected EnemyMovement EnemyMovement { get; private set; }
        public PhysicsImpactEffector PhysicsImpactEffector { get; private set; }


        public Vector2 StartPosition { get; private set; }

        protected virtual void Awake()
        {
            EnemyMovement = GetComponent<EnemyMovement>();
            AnimationsHandler = GetComponentInChildren<EnemyAnimationsHandler>();
        }

        public void OnSpawned()
        {
            CurrentHealth = EnemyDataSO.StartHealth;
            StartPosition = transform.position;

            InitStateMachine();

            if (EnemyDataSO.IsBoss)
                transform.localScale = Vector3.one * 2f;
            else
                transform.localScale = Vector3.one;
            
            GetComponentInChildren<HealthBarGUI>(true).gameObject.SetActive(EnemyDataSO.IsBoss);
            
            AudioManager.I.PlayAudio(SFXAudioEnum.ENEMY_SPAWN);
        }

        private void OnDisable() => StateMachine.CurrentState.OnExit();

        private void Start()
        {
            PhysicsImpactEffector = new PhysicsImpactEffector(EnemyMovement.Rigidbody, EnemyMovement);
        }

        private void Update() => StateMachine.Tick();

        protected abstract void InitStateMachine();

        public virtual void TakeDamage(int count)
        {
            CurrentHealth -= count;
            Debug.Log($"{count}/{EnemyDataSO.StartHealth}");
            HealthChanged?.Invoke(CurrentHealth);
            
            AudioManager.I.PlayAudio(SFXAudioEnum.ENEMY_HIT);
        }

        public void SetData(EnemyDataSO enemyData)
        {
            EnemyDataSO = enemyData;

            if (enemyData.IsBoss)
                Debug.Log(("booss"));
        }

        public void SetSpawnData(IEnemySpawnData spawnData) => SpawnData = spawnData;

        protected bool IsInStartArea() =>
            Vector3.Distance(transform.position, StartPosition) < EnemyDataSO.StartAreaRadius;

        protected bool InSightWithPlayer() =>
            GetDistanceToPlayer() < EnemyDataSO.FollowRadius;

        protected bool InMeleeAttackReachWithPlayer() =>
            GetDistanceToPlayer() < EnemyDataSO.MeleeAttackRadius;

        protected float GetDistanceToPlayer() =>
            Vector3.Distance(Player.Current.transform.position, transform.position);


        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, EnemyDataSO.MeleeAttackRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(StartPosition, EnemyDataSO.StartAreaRadius);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, EnemyDataSO.FollowRadius);
        }
    }
}