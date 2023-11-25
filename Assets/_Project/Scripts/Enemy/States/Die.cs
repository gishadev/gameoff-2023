using gishadev.tools.StateMachine;
using UnityEngine;

namespace gameoff.Enemy.States
{
    public class Die : IState
    {
        private readonly Enemy _enemy;

        public Die(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _enemy.gameObject.SetActive(false);
        }

        public void OnExit()
        {
        }
    }
}