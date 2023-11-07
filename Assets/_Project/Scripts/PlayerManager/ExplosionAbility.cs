using gishadev.tools.Effects;
using UnityEngine;

namespace gameoff.PlayerManager
{
    public class ExplosionAbility : IAbility
    {
        private readonly Player _player;

        public ExplosionAbility(Player player)
        {
            _player = player;
        }
        
        public void Trigger()
        {
            Debug.Log("KABOOM");
            VFXEmitter.I.EmitAt(VisualEffectsEnum.EXPLOSION_ABILITY_VFX, _player.transform.position, Quaternion.identity);
        }
    }

    public interface IAbility
    {
        void Trigger();
    }
}