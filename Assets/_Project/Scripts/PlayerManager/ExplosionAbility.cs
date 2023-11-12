using gishadev.tools.Effects;
using UnityEngine;

namespace gameoff.PlayerManager
{
    public class ExplosionAbility : IAbility
    {
        private readonly Player _player;
        private readonly int _projectileDamage;

        private readonly int _projectileCount = 10;

        public ExplosionAbility(Player player, int projectileDamage)
        {
            _player = player;
            _projectileDamage = projectileDamage;
        }

        public void Trigger()
        {
            SpawnProjectilesInCircle();
            VFXEmitter.I.EmitAt(VisualEffectsEnum.EXPLOSION_ABILITY_VFX, _player.transform.position,
                Quaternion.identity);
        }

        private void SpawnProjectilesInCircle()
        {
            for (int i = 0; i < _projectileCount; i++)
            {
                float angle = i * Mathf.PI * 2f / _projectileCount;

                // Calculate the position in the circle using polar coordinates
                float x = Mathf.Cos(angle) * .05f;
                float y = Mathf.Sin(angle) * .05f;
                var position = _player.transform.position + new Vector3(x, y, _player.transform.position.z);
                var rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);

                // Instantiate the object at the calculated position
                var spawnedProjectile = OtherEmitter.I.EmitAt(OtherPoolEnum.EXPLOSION_PROJECTILE, position, rotation)
                    .GetComponent<BlasterProjectile>();
                spawnedProjectile.SetDamage(_projectileDamage);
            }
        }
    }

    public interface IAbility
    {
        void Trigger();
    }
}