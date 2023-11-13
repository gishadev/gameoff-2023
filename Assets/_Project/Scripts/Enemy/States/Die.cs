using gishadev.tools.StateMachine;
using UnityEngine;

namespace gameoff.Enemy.States
{
    public class Die : IState
    {
        private readonly Roach _roach;

        public Die(Roach roach)
        {
            _roach = roach;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _roach.gameObject.SetActive(false);
        }

        public void OnExit()
        {
        }
    }
}