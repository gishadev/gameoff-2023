using System.Collections;
using gameoff.PlayerManager;
using gishadev.tools.StateMachine;
using UnityEngine;

namespace gameoff.Enemy.States
{
    public class MoveAway : IState
    {
        private readonly ShootEnemy _shootEnemy;
        private readonly EnemyMovement _enemyMovement;
        private Player _player;

        public MoveAway(ShootEnemy shootEnemy, EnemyMovement enemyMovement)
        {
            _shootEnemy = shootEnemy;
            _enemyMovement = enemyMovement;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _player = Player.Current;
            _enemyMovement.StartCoroutine(MoveAwayRoutine());
        }

        public void OnExit()
        {
            _enemyMovement.StopAllCoroutines();
            _enemyMovement.ChangeMoveSpeed(_shootEnemy.EnemyDataSO.MoveSpeed);
        }

        private IEnumerator MoveAwayRoutine()
        {
            while (true)
            {
                var moveAwayDirection = (_shootEnemy.transform.position - _player.transform.position).normalized;
                var fleePosition = _shootEnemy.transform.position + moveAwayDirection * 100f;
                _enemyMovement.SetDestination(fleePosition);
                yield return new WaitForSeconds(0.6f);
            }
        }
    }
}