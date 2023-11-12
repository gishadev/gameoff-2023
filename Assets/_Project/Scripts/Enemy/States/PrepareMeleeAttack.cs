using gishadev.tools.StateMachine;
using UnityEngine;

namespace gameoff.Enemy.States
{
    public class PrepareMeleeAttack : IState
    {
        private float _startTime;
        
        public void Tick()
        {
        }

        public void OnEnter()
        {
            _startTime = Time.time;
        }

        public void OnExit()
        {
        }

        public float GetElapsedTime() => Time.time - _startTime;
    }
}