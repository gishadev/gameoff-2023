using System;
using gameoff.Enemy.SOs;
using gameoff.Enemy.States;
using gameoff.PlayerManager;
using gishadev.tools.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace gameoff.Enemy
{
    public class FastEnemy : Enemy
    {
        [field: SerializeField, InlineEditor] public FastEnemyDataSO FastData { get; private set; }

        public override EnemyDataSO EnemyDataSO => FastData;

        public bool Damaged { get; set; }

        protected override void InitStateMachine()
        {
            StateMachine = new StateMachine();

            var idle = new Idle();
            var follow = new Follow(this, EnemyMovement);
            var prepareToAttack = new PrepareMeleeAttack();
            var attack = new MeleeAttack(this);
            var flee = new Flee(this, EnemyMovement);
            var die = new Die(this);

            At(idle, follow, InSightWithPlayer);
            At(follow, prepareToAttack, InAttackReachWithPlayer);
            At(flee, follow, IsFleeDelayElapsed);

            At(prepareToAttack, follow, () => !InAttackReachWithPlayer());
            At(prepareToAttack, attack, IsAttackDelayElapsed);

            At(attack, idle, () => true);

            Aat(die, () => CurrentHealth <= 0);
            Aat(flee, () => Damaged);

            StateMachine.SetState(idle);

            bool InSightWithPlayer() =>
                Vector3.Distance(Player.Current.transform.position, transform.position) < FastData.FollowRadius;

            bool InAttackReachWithPlayer() =>
                Vector3.Distance(Player.Current.transform.position, transform.position) < FastData.MeleeAttackRadius;

            bool IsAttackDelayElapsed() => prepareToAttack.GetElapsedTime() > FastData.MeleeAttackDelay;
            bool IsFleeDelayElapsed() => flee.GetElapsedTime() > FastData.FleeTime;

            void At(IState from, IState to, Func<bool> cond) => StateMachine.AddTransition(from, to, cond);
            void Aat(IState to, Func<bool> cond) => StateMachine.AddAnyTransition(to, cond);
        }

        public override void TakeDamage(int count)
        {
            base.TakeDamage(count);

            if (count >= FastData.MinDamageToFlee)
                Damaged = true;
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, FastData.FollowRadius);
        }
    }
}