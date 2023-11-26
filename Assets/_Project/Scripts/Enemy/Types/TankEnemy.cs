using System;
using gameoff.Enemy.SOs;
using gameoff.Enemy.States;
using gameoff.PlayerManager;
using gishadev.tools.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace gameoff.Enemy
{
    public class TankEnemy : Enemy
    {
        [field: SerializeField, InlineEditor] public TankEnemyDataSO TankData { get; private set; }

        public override EnemyDataSO EnemyDataSO => TankData;

        private EnemyProjectileShield _projectileShield;

        protected override void Awake()
        {
            base.Awake();
            _projectileShield = GetComponentInChildren<EnemyProjectileShield>();
            _projectileShield.transform.localScale = Vector3.one * TankData.ProjectileShieldRadius * 2f;
        }

        protected override void InitStateMachine()
        {
            StateMachine = new StateMachine();

            var prepareToAttack = new PrepareMeleeAttack();
            var attack = new MeleeAttack(this, EnemyMovement);
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
            
            At(follow, prepareToAttack, InAttackReachWithPlayer);

            At(prepareToAttack, follow, () => !InAttackReachWithPlayer());
            At(prepareToAttack, attack, IsAttackDelayElapsed);

            At(attack, idle, () => true);

            Aat(die, () => CurrentHealth <= 0);

            StateMachine.SetState(idle);

            bool InAttackReachWithPlayer() =>
                Vector3.Distance(Player.Current.transform.position, transform.position) < TankData.MeleeAttackRadius;
            bool IsAttackDelayElapsed() => prepareToAttack.GetElapsedTime() > TankData.MeleeAttackDelay;

            void At(IState from, IState to, Func<bool> cond) => StateMachine.AddTransition(from, to, cond);
            void Aat(IState to, Func<bool> cond) => StateMachine.AddAnyTransition(to, cond);
        }


        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, TankData.FollowRadius);
        }
    }
}