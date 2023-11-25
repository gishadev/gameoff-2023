using System;
using gameoff.Enemy.SOs;
using gameoff.Enemy.States;
using gameoff.PlayerManager;
using gishadev.tools.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace gameoff.Enemy
{
    public class DefaultEnemy : Enemy
    {
        [field: SerializeField, InlineEditor] public DefaultRoachDataSO RoachData { get; private set; }

        public override EnemyDataSO EnemyDataSO => RoachData;

        protected override void InitStateMachine()
        {
            StateMachine = new StateMachine();

            var idle = new Idle();
            var follow = new Follow(this, EnemyMovement);
            var prepareToAttack = new PrepareMeleeAttack();
            var attack = new MeleeAttack(this);
            var die = new Die(this);

            At(idle, follow, InSightWithPlayer);
            At(follow, prepareToAttack, InAttackReachWithPlayer);

            At(prepareToAttack, follow, () => !InAttackReachWithPlayer());
            At(prepareToAttack, attack, IsAttackDelayElapsed);

            At(attack, idle, () => true);

            Aat(die, () => CurrentHealth <= 0);

            StateMachine.SetState(idle);

            bool InSightWithPlayer() =>
                Vector3.Distance(Player.Current.transform.position, transform.position) < RoachData.FollowRadius;

            bool InAttackReachWithPlayer() =>
                Vector3.Distance(Player.Current.transform.position, transform.position) < RoachData.MeleeAttackRadius;

            bool IsAttackDelayElapsed() => prepareToAttack.GetElapsedTime() > RoachData.MeleeAttackDelay;

            void At(IState from, IState to, Func<bool> cond) => StateMachine.AddTransition(from, to, cond);
            void Aat(IState to, Func<bool> cond) => StateMachine.AddAnyTransition(to, cond);
        }


        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, RoachData.FollowRadius);
        }
    }
}