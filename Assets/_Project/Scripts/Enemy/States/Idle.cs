using gishadev.tools.StateMachine;

namespace gameoff.Enemy.States
{
    public class Idle : IState
    {
        private readonly EnemyMovement _enemyMovement;

        public Idle(EnemyMovement enemyMovement)
        {
            _enemyMovement = enemyMovement;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _enemyMovement.Stop();
        }

        public void OnExit()
        {
        }
    }
}