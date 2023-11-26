using System;
using gameoff.Enemy.SOs;
using gameoff.Enemy.States;
using gishadev.tools.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace gameoff.Enemy
{
    public class TankEnemy : Enemy
    {
        [field: SerializeField, InlineEditor] public TankEnemyDataSO TankData { get; private set; }

        public override EnemyDataSO EnemyDataSO
        {
            get => TankData;
            protected set => TankData = (TankEnemyDataSO) value;
        }

        private EnemyProjectileShield _projectileShield;

        protected override void Awake()
        {
            base.Awake();
            _projectileShield = GetComponentInChildren<EnemyProjectileShield>();
            _projectileShield.transform.localScale =
                Vector3.one * TankData.ProjectileShieldRadius * 2f;
        }

        protected override void InitStateMachine()
        {
            StateMachine = new StateMachine();

            var meleeAttack = new MeleeAttack(this, EnemyMovement);
            var die = new Die(this);

            #region Idle/Follow/Return

            var idle = new Idle(EnemyMovement);
            var follow = new Follow(this, EnemyMovement);
            var returnToStart = new ReturnToStart(this, EnemyMovement);
            At(idle, follow, InSightWithPlayer);
            At(follow, returnToStart, () => !InSightWithPlayer());
            At(returnToStart, idle, IsInStartArea);
            At(returnToStart, follow, InSightWithPlayer);

            #endregion

            At(follow, meleeAttack, InMeleeAttackReachWithPlayer);
            At(meleeAttack, follow, () => !InMeleeAttackReachWithPlayer());

            Aat(die, () => CurrentHealth <= 0);

            StateMachine.SetState(idle);

            void At(IState from, IState to, Func<bool> cond) => StateMachine.AddTransition(from, to, cond);
            void Aat(IState to, Func<bool> cond) => StateMachine.AddAnyTransition(to, cond);
        }
    }
}