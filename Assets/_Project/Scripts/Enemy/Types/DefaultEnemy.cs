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