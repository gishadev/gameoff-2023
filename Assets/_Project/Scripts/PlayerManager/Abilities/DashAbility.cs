using System.Threading;
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
    public class DashAbility : IAbility
    {
        public AbilityDataSO AbilityDataSO => _dashData;
        public bool IsUsing { get; private set; }

        private readonly PlayerMovement _playerMovement;

        private DashAbilityDataSO _dashData;
        private CancellationTokenSource _dashingCTS;
        private ICreepClearing _creepClearing;

        public DashAbility(PlayerMovement playerMovement, DiContainer diContainer)
        {
            _playerMovement = playerMovement;
            _dashData = diContainer.Resolve<GameDataSO>().DashAbilityDataSO;
            _creepClearing = diContainer.Resolve<ICreepClearing>();
        }

        public async void Trigger()
        {
            if (_playerMovement.MoveInputVector.magnitude == 0)
                return;

            IsUsing = true;
            _dashingCTS = new CancellationTokenSource();

            _playerMovement.DisableDefaultMovement();
            _creepClearing.ClearCreep(_playerMovement.transform.position, _dashData.StartClearRadius);
            _playerMovement.Rigidbody.AddForce(_playerMovement.MoveInputVector * _dashData.DashingPower,
                ForceMode2D.Impulse);
            _playerMovement.TrailRenderer.emitting = true;
            
            AudioManager.I.PlayAudio(SFXAudioEnum.ABILITY_DASH);
            VFXEmitter.I.EmitAt(VisualEffectsEnum.DASH_VFX, _playerMovement.transform.position, Quaternion.identity);

            await UniTask.WaitForSeconds(_dashData.DashingTime, cancellationToken: _dashingCTS.Token)
                .SuppressCancellationThrow();

            _creepClearing.ClearCreep(_playerMovement.transform.position, _dashData.EndClearRadius);
            _playerMovement.TrailRenderer.emitting = false;
            IsUsing = false;
            _playerMovement.EnableDefaultMovement();
        }

        public void Cancel()
        {
            _dashingCTS?.Cancel();
            _playerMovement.TrailRenderer.emitting = false;
            IsUsing = false;
        }
    }
}