using System;
using gameoff.Enemy.States;
using gameoff.PlayerManager;
using gishadev.tools.StateMachine;
using UnityEngine;

namespace gameoff.Enemy
{
    [RequireComponent(typeof(EnemyMovement))]
    public class Roach : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public int StartHealth { private set; get; } = 2;

        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float followRadius = 5f;
        [Space] [SerializeField] private float attackRadius = 1f;
        [SerializeField] private float attackDelay = 0.7f;
        [field: SerializeField] public int AttackDamage { private set; get; } = 1;

        public int CurrentHealth { get; private set; }

        public float MoveSpeed => moveSpeed;

        public EnemySpawnData SpawnData { private set; get; }

        private EnemyMovement _enemyMovement;
        private StateMachine _stateMachine;

        private void Awake()
        {
            _enemyMovement = GetComponent<EnemyMovement>();
        }

        private void OnEnable()
        {
            CurrentHealth = StartHealth;
            InitStateMachine();
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        private void InitStateMachine()
        {
            _stateMachine = new StateMachine();

            var idle = new Idle();
            var follow = new Follow(this, _enemyMovement);
            var prepareToAttack = new PrepareMeleeAttack();
            var attack = new Attack(this);
            var die = new Die(this);

            At(idle, follow, InSightWithPlayer);
            At(follow, prepareToAttack, InAttackReachWithPlayer);

            At(prepareToAttack, follow, () => !InAttackReachWithPlayer());
            At(prepareToAttack, attack, IsAttackDelayElapsed);

            At(attack, idle, () => true);

            Aat(die, () => CurrentHealth <= 0);

            _stateMachine.SetState(idle);

            bool InSightWithPlayer() =>
                Vector3.Distance(Player.Current.transform.position, transform.position) < followRadius;

            bool InAttackReachWithPlayer() =>
                Vector3.Distance(Player.Current.transform.position, transform.position) < attackRadius;

            bool IsAttackDelayElapsed() => prepareToAttack.GetElapsedTime() > attackDelay;

            void At(IState from, IState to, Func<bool> cond) => _stateMachine.AddTransition(from, to, cond);
            void Aat(IState to, Func<bool> cond) => _stateMachine.AddAnyTransition(to, cond);
        }

        public void TakeDamage(int count)
        {
            CurrentHealth -= count;
        }

        public void SetSpawnData(EnemySpawnData spawnData) => SpawnData = spawnData;


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, followRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}