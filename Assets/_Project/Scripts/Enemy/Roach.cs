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
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float followRadius = 5f;
        [SerializeField] private float attackRadius = 1f;

        public int CurrentHealth { get; private set; } = 2;

        public float MoveSpeed => moveSpeed;

        private EnemyMovement _enemyMovement;
        private StateMachine _stateMachine;

        private void Awake()
        {
            _enemyMovement = GetComponent<EnemyMovement>();
        }

        private void Start()
        {
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
            var attack = new Attack();
            var die = new Die(this);

            At(idle, follow, InSightWithPlayer);
            At(follow, attack, InAttackReachWithPlayer);
            At(attack, idle, () => true);

            Aat(die, () => CurrentHealth <= 0);

            _stateMachine.SetState(idle);

            bool InSightWithPlayer() =>
                Vector3.Distance(Player.Current.transform.position, transform.position) < followRadius;

            bool InAttackReachWithPlayer() =>
                Vector3.Distance(Player.Current.transform.position, transform.position) < attackRadius;

            void At(IState from, IState to, Func<bool> cond) => _stateMachine.AddTransition(from, to, cond);
            void Aat(IState to, Func<bool> cond) => _stateMachine.AddAnyTransition(to, cond);
        }

        public void TakeDamage(int count)
        {
            CurrentHealth -= count;
        }

        private void OnParticleCollision(GameObject other)
        {
            TakeDamage(1);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, followRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }

    public interface IDamageable
    {
        int CurrentHealth { get; }
        void TakeDamage(int count);
    }
}