using gameoff.PlayerManager;
using gameoff.World;
using UnityEngine;
using Zenject;

namespace gameoff.Infrastructure
{
    public class GameSceneController : MonoBehaviour
    {
        [Inject] private ICreepClearing _creepClearing;
        [Inject] private IPlayerUpgradesController _playerUpgradesController;

        private void Awake()
        {
            _creepClearing.Init();
            _playerUpgradesController.Init();
        }

        private void OnDisable()
        {
            _playerUpgradesController.Dispose();
        }
    }
}