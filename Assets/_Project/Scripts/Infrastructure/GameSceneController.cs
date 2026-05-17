using gameoff.PlayerManager;
using gameoff.SavingLoading;
using gameoff.World;
using UnityEngine;
using Zenject;

namespace gameoff.Infrastructure
{
    public class GameSceneController : MonoBehaviour
    {
        [Inject] private ICreepClearing _creepClearing;
        [Inject] private IPlayerUpgradesController _playerUpgradesController;
        [Inject] private ISaveLoadController _saveLoadController;

        private void Awake()
        {
            if (_saveLoadController.CurrentSaveData == null)
                _saveLoadController.LoadGame();

            _creepClearing.Init();
            _playerUpgradesController.Init();
        }

        private void OnDisable()
        {
            _playerUpgradesController.Dispose();
        }
    }
}