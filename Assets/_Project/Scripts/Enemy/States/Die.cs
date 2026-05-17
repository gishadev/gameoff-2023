using gishadev.tools.Audio;
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
            _enemy.gameObject.SetActive(false);
            
            VFXEmitter.I.EmitAt(VisualEffectsEnum.ENEMY_DIE_VFX, _enemy.transform.position, Quaternion.identity);
            AudioManager.I.PlayAudio(SFXAudioEnum.ENEMY_HIT);
        }

        public void OnExit()
        {
        }
    }
}