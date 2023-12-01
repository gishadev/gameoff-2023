using Cysharp.Threading.Tasks;
using gameoff.Core;
using gameoff.PlayerManager;
using gameoff.World;
using gishadev.tools.Audio;
using gishadev.tools.Effects;
using UnityEngine;
using Zenject;

namespace gameoff.PlayerManager
{
    public class ExplosionAbility : IAbility
    {
        public bool IsUsing { get; private set; }
        public AbilityDataSO AbilityDataSO => _explosionData;
        
        private readonly ExplosionAbilityDataSO _explosionData;
        private readonly Player _player;
        private readonly DiContainer _diContainer;
        private readonly ICreepClearing _creepClearing;
        
        private Vector3 _triggeredPosition;
        
        public ExplosionAbility(Player player, DiContainer diContainer)
        {
            _diContainer = diContainer;
            _creepClearing = _diContainer.Resolve<ICreepClearing>();
            _explosionData = _diContainer.Resolve<GameDataSO>().ExplosionAbilityDataSO;
            
            _player = player;
        }


        public void Trigger()
        {
            IsUsing = true;
            _triggeredPosition = _player.transform.position;
            SpawnProjectilesInCircle();
            ClearAreaInCircleAsync();
            
            IsUsing = false;
            
            AudioManager.I.PlayAudio(SFXAudioEnum.ABILITY_EXPLOSION);
            VFXEmitter.I.EmitAt(VisualEffectsEnum.EXPLOSION_ABILITY_VFX, _player.transform.position,
                Quaternion.identity);
        }

        public void Cancel()
        {
        }

        private void SpawnProjectilesInCircle()
        {
            for (int i = 0; i < _explosionData.ProjectileCount; i++)
            {
                float angle = i * Mathf.PI * 2f / _explosionData.ProjectileCount;

                // Calculate the position in the circle using polar coordinates
                float x = Mathf.Cos(angle) * .05f;
                float y = Mathf.Sin(angle) * .05f;
                var position = _player.transform.position + new Vector3(x, y, _player.transform.position.z);
                var rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);

                // Instantiate the object at the calculated position
                var spawnedProjectile = OtherEmitter.I.EmitAt(OtherPoolEnum.EXPLOSION_PROJECTILE, position, rotation)
                    .GetComponent<ExplosionAbilityProjectile>();
                _diContainer.InjectGameObject(spawnedProjectile.gameObject);
                spawnedProjectile.SetDamage(_explosionData.ProjectileDamage);
            }
        }

        private async void ClearAreaInCircleAsync()
        {
            var rStep = _explosionData.ClearMaxRadius / _explosionData.ClearIterations;
            var clearRadius = rStep;
            var iterationTime = _explosionData.FullClearExpandingTime / _explosionData.ClearIterations;
            for (int i = 0; i < _explosionData.ClearIterations; i++)
            {
                _creepClearing.ClearCreep(_triggeredPosition, Mathf.RoundToInt(clearRadius));
                await UniTask.WaitForSeconds(iterationTime);
                clearRadius += rStep;
            }
        }
    }
}