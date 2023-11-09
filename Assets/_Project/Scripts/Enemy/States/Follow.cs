using System.Collections;
using gameoff.PlayerManager;
using gishadev.tools.StateMachine;
using UnityEngine;

namespace gameoff.Enemy.States
{
    public class Follow : IState
    {
        private readonly Roach _roach;
        private readonly EnemyMovement _enemyMovement;

        public Follow(Roach roach, EnemyMovement enemyMovement)
        {
            _roach = roach;
            _enemyMovement = enemyMovement;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _enemyMovement.ChangeMoveSpeed(_roach.MoveSpeed);
            _enemyMovement.StartCoroutine(FollowRoutine());
        }

        public void OnExit()
        {
            _enemyMovement.StopAllCoroutines();
        }
        
        private IEnumerator FollowRoutine()
        {
            while (true)
            {
                _enemyMovement.SetDestination(Player.Current.transform.position);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}