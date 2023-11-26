using System.Threading;
using Cysharp.Threading.Tasks;
using gameoff.PlayerManager;
using gishadev.tools.StateMachine;

namespace gameoff.Enemy.States
{
    public class MeleeAttack : StateWithElapsedTime
    {
        private readonly Enemy _enemy;
        private readonly EnemyMovement _enemyMovement;
        private CancellationTokenSource _cts;
        private Player _player;
        
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
            _player = Player.Current;

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
                _player.TakeDamage(_enemy.EnemyDataSO.MeleeAttackDamage);
            }
        }
    }
}