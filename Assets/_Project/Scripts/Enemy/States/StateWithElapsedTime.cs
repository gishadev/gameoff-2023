using gishadev.tools.StateMachine;
using UnityEngine;

namespace gameoff.Enemy.States
{
    public abstract class StateWithElapsedTime : IState
    {
        private float _startTime;

        public abstract void Tick();

        public abstract void OnEnter();

        public abstract void OnExit();

        protected void SetStartTime() => _startTime = Time.time;
        public float GetElapsedTime() => Time.time - _startTime;
    }
}