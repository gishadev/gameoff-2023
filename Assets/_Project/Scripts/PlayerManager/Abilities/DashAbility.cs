using System.Threading;
using Cysharp.Threading.Tasks;
using gameoff.Core;
using gameoff.PlayerManager.SOs;
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

        public DashAbility(PlayerMovement playerMovement, DiContainer diContainer)
        {
            _playerMovement = playerMovement;
            _dashData = diContainer.Resolve<GameDataSO>().DashAbilityDataSO;
        }
        
        public async void Trigger()
        {
            IsUsing = true;
            _playerMovement.SetDefaultMovement(false);
            
            _dashingCTS = new CancellationTokenSource();
            _playerMovement.Rigidbody.AddForce(_playerMovement.MoveInputVector * _dashData.DashingPower, ForceMode2D.Impulse);
            _playerMovement.TrailRenderer.emitting = true;
            
            await UniTask.WaitForSeconds(_dashData.DashingTime, cancellationToken: _dashingCTS.Token)
                .SuppressCancellationThrow();

            _playerMovement.TrailRenderer.emitting = false;
            IsUsing = false;
            _playerMovement.SetDefaultMovement(true);
        }

        public void Cancel()
        {
            _dashingCTS?.Cancel();
            _playerMovement.TrailRenderer.emitting = false;
            IsUsing = false;
        }
    }
}