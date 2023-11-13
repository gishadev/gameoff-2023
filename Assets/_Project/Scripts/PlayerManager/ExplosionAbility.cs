using Cysharp.Threading.Tasks;
using gameoff.World;
using gishadev.tools.Effects;
using UnityEngine;
using Zenject;

namespace gameoff.PlayerManager
{
    public class ExplosionAbility : IAbility
    {
        private readonly Player _player;
        private readonly DiContainer _diContainer;
        private readonly SpecialAbilitySettings _settings;
        private readonly ICreepClearing _creepClearing;
        private Vector3 _triggeredPosition;
        
        public ExplosionAbility(Player player, DiContainer diContainer, SpecialAbilitySettings settings)
        {
            _diContainer = diContainer;
            _settings = settings;
            _creepClearing = _diContainer.Resolve<ICreepClearing>();

            _player = player;
        }

        public void Trigger()
        {
            _triggeredPosition = _player.transform.position;
            SpawnProjectilesInCircle();
            ClearAreaInCircleAsync();
            VFXEmitter.I.EmitAt(VisualEffectsEnum.EXPLOSION_ABILITY_VFX, _player.transform.position,
                Quaternion.identity);
        }

        private void SpawnProjectilesInCircle()
        {
            for (int i = 0; i < _settings.ProjectileCount; i++)
            {
                float angle = i * Mathf.PI * 2f / _settings.ProjectileCount;

                // Calculate the position in the circle using polar coordinates
                float x = Mathf.Cos(angle) * .05f;
                float y = Mathf.Sin(angle) * .05f;
                var position = _player.transform.position + new Vector3(x, y, _player.transform.position.z);
                var rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);

                // Instantiate the object at the calculated position
                var spawnedProjectile = OtherEmitter.I.EmitAt(OtherPoolEnum.EXPLOSION_PROJECTILE, position, rotation)
                    .GetComponent<ExplosionAbilityProjectile>();
                _diContainer.InjectGameObject(spawnedProjectile.gameObject);
                spawnedProjectile.SetDamage(_settings.ProjectileDamage);
            }
        }

        private async void ClearAreaInCircleAsync()
        {
            var rStep = _settings.ClearMaxRadius / _settings.ClearIterations;
            var clearRadius = rStep;
            var iterationTime = _settings.FullClearExpandingTime / _settings.ClearIterations;
            for (int i = 0; i < _settings.ClearIterations; i++)
            {
                _creepClearing.ClearCreep(_triggeredPosition, Mathf.RoundToInt(clearRadius));
                await UniTask.WaitForSeconds(iterationTime);
                clearRadius += rStep;
            }
        }
    }

    public interface IAbility
    {
        void Trigger();
    }
}