using System.Threading;
using Cysharp.Threading.Tasks;
using gameoff.PlayerManager;

namespace gameoff.Enemy.States
{
    public class MeleeAttack : StateWithElapsedTime
    {
        private readonly Enemy _enemy;
        private readonly EnemyMovement _enemyMovement;
        private CancellationTokenSource _cts;

        public MeleeAttack(Enemy enemy, EnemyMovement enemyMovement)
        {
            _enemy = enemy;
            _enemyMovement = enemyMovement;
        }

        public override void Tick()
        {
        }

        public override void OnEnter()
        {
            _cts = new CancellationTokenSource();
            _enemyMovement.Stop();

            MeleeAttackAsync();
        }

        public override void OnExit()
        {
            _cts.Cancel();
        }

        private async void MeleeAttackAsync()
        {
            while (!_cts.IsCancellationRequested)
            {
                await UniTask.WaitForSeconds(_enemy.EnemyDataSO.MeleeAttackDelay, cancellationToken: _cts.Token)
                    .SuppressCancellationThrow();
                if (_cts.IsCancellationRequested)
                    return;
                
                _enemy.AnimationsHandler.TriggerAttackAnimation(Player.Current.transform);
                Player.Current.TakeDamage(_enemy.EnemyDataSO.MeleeAttackDamage);
                Player.Current.PhysicsImpactEffector.Act(_enemy.transform.position, 20f);
            }
        }
    }
}