using gameoff.PlayerManager;
using gishadev.tools.StateMachine;

namespace gameoff.Enemy.States
{
    public class MeleeAttack : IState
    {
        private readonly Enemy _enemy;
        private readonly EnemyMovement _enemyMovement;

        public MeleeAttack(Enemy enemy, EnemyMovement enemyMovement)
        {
            _enemy = enemy;
            _enemyMovement = enemyMovement;
        }
        
        public void Tick()
        {
        }

        public void OnEnter()
        {
            _enemyMovement.Stop();
            var player = Player.Current;
            player.TakeDamage(_enemy.EnemyDataSO.MeleeAttackDamage);
        }

        public void OnExit()
        {
        }
    }
}