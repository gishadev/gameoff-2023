using System.Collections;
using gishadev.tools.StateMachine;
using UnityEngine;

namespace gameoff.Enemy.States
{
    public class ReturnToStart : IState
    {
        private readonly Enemy _enemy;
        private readonly EnemyMovement _enemyMovement;

        public ReturnToStart(Enemy enemy, EnemyMovement enemyMovement)
        {
            _enemy = enemy;
            _enemyMovement = enemyMovement;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _enemyMovement.ChangeMoveSpeed(_enemy.EnemyDataSO.MoveSpeed);
            _enemyMovement.StartCoroutine(ReturnRoutine());
        }

        public void OnExit()
        {
            _enemyMovement.StopAllCoroutines();
        }

        private IEnumerator ReturnRoutine()
        {
            while (true)
            {
                _enemyMovement.SetDestination(_enemy.StartPosition);
                yield return new WaitForSeconds(2f);
            }
        }
    }
}