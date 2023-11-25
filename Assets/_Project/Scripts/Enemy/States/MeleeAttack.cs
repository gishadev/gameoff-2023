using gameoff.PlayerManager;
using gishadev.tools.StateMachine;

namespace gameoff.Enemy.States
{
    public class MeleeAttack : IState
    {
        private readonly Enemy _enemy;

        public MeleeAttack(Enemy enemy)
        {
            _enemy = enemy;
        }
        
        public void Tick()
        {
        }

        public void OnEnter()
        {
            var player = Player.Current;
            player.TakeDamage(_enemy.EnemyDataSO.MeleeAttackDamage);
        }

        public void OnExit()
        {
        }
    }
}