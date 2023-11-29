using System.Collections;
using gameoff.PlayerManager;
using gishadev.tools.StateMachine;
using UnityEngine;

namespace gameoff.Enemy.States
{
    public class Follow : IState
    {
        private readonly Enemy _enemy;
        private readonly EnemyMovement _enemyMovement;
        private Player _player;

        public Follow(Enemy enemy, EnemyMovement enemyMovement)
        {
            _enemy = enemy;
            _enemyMovement = enemyMovement;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _player = Player.Current;
            _enemyMovement.ChangeMoveSpeed(_enemy.EnemyDataSO.MoveSpeed);
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
                yield return new WaitForSeconds(0.5f);
                _enemyMovement.SetDestination(_player.transform.position);
            }
        }
    }
}