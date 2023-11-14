using gameoff.PlayerManager;
using gishadev.tools.StateMachine;

namespace gameoff.Enemy.States
{
    public class Attack : IState
    {
        private readonly Roach _roach;

        public Attack(Roach roach)
        {
            _roach = roach;
        }
        
        public void Tick()
        {
        }

        public void OnEnter()
        {
            var player = Player.Current;
            player.TakeDamage(_roach.AttackDamage);
        }

        public void OnExit()
        {
        }
    }
}