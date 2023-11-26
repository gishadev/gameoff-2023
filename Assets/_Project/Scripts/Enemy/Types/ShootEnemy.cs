using System;
using gameoff.Enemy.SOs;
using gameoff.Enemy.States;
using gishadev.tools.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace gameoff.Enemy
{
    public class ShootEnemy : Enemy
    {
        [field: SerializeField, InlineEditor] public ShootEnemyDataSO ShootData { get; private set; }
        [field: SerializeField] public Transform ShootPoint { private set; get; }

        public override EnemyDataSO EnemyDataSO => ShootData;

        protected override void InitStateMachine()
        {
            StateMachine = new StateMachine();

            var shooting = new Shooting(this, EnemyMovement);
            var moveAway = new MoveAway(this, EnemyMovement);
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

            At(follow, shooting, IsDistanceToPlayerSuitableForShoot);
            At(moveAway, shooting, IsDistanceToPlayerSuitableForShoot);
            At(shooting, follow, () => !IsDistanceToPlayerSuitableForShoot());

            Aat(moveAway, IsPlayerTooClose);
            Aat(die, () => CurrentHealth <= 0);

            StateMachine.SetState(idle);

            bool IsDistanceToPlayerSuitableForShoot() => GetDistanceToPlayer() > ShootData.ShootMinRadius &&
                                                         GetDistanceToPlayer() < ShootData.ShootMaxRadius;

            bool IsPlayerTooClose() => GetDistanceToPlayer() < ShootData.PreferableDistanceToPlayer;
            void At(IState from, IState to, Func<bool> cond) => StateMachine.AddTransition(from, to, cond);
            void Aat(IState to, Func<bool> cond) => StateMachine.AddAnyTransition(to, cond);
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, ShootData.ShootMinRadius);
            Gizmos.DrawWireSphere(transform.position, ShootData.ShootMaxRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, ShootData.PreferableDistanceToPlayer);
        }
    }
}