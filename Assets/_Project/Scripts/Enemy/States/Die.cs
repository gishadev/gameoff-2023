using gishadev.tools.Effects;
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
            VFXEmitter.I.EmitAt(VisualEffectsEnum.ENEMY_DIE_VFX, _enemy.transform.position, Quaternion.identity);
            _enemy.gameObject.SetActive(false);
        }

        public void OnExit()
        {
        }
    }
}