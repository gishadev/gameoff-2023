using System.Threading;
using Cysharp.Threading.Tasks;
using gameoff.Core;
using gameoff.Enemy.Projectiles;
using gameoff.PlayerManager;
using gishadev.tools.Effects;
using gishadev.tools.StateMachine;
using UnityEngine;

namespace gameoff.Enemy.States
{
    public class Shooting : IState
    {
        private readonly ShootEnemy _shootEnemy;
        private readonly EnemyMovement _enemyMovement;
        private Player _player;
        private CancellationTokenSource _cts;

        public Shooting(ShootEnemy shootEnemy, EnemyMovement enemyMovement)
        {
            _shootEnemy = shootEnemy;
            _enemyMovement = enemyMovement;
        }

        public void Tick()
        {
            _enemyMovement.FlipTowardsPosition(_player.transform.position);
        }

        public void OnEnter()
        {
            _enemyMovement.Stop();
            _cts = new CancellationTokenSource();
            _player = Player.Current;
            ShootAsync();
        }

        public void OnExit()
        {
            _cts.Cancel();
        }

        private async void ShootAsync()
        {
            while (!_cts.IsCancellationRequested)
            {
                await UniTask.WaitForSeconds(_shootEnemy.ShootData.ShootDelay, cancellationToken: _cts.Token)
                    .SuppressCancellationThrow();
                if (_cts.IsCancellationRequested)
                    return;

                var shootDir = (_player.transform.position - _shootEnemy.transform.position).normalized;
                var shootRotation =
                    Quaternion.AngleAxis(Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg, Vector3.forward);

                OtherPoolEnum projectilePoolKey = _shootEnemy.EnemyDataSO.IsBoss
                    ? OtherPoolEnum.BOSS_PROJECTILE
                    : OtherPoolEnum.ENEMY_PROJECTILE;
                
                var projectile = OtherEmitter.I
                    .EmitAt(projectilePoolKey, _shootEnemy.ShootPoint.position, shootRotation)
                    .GetComponent<EnemyProjectile>();
                projectile.SetDamage(_shootEnemy.ShootData.ShootProjectileDamage);
            }
        }
    }
}