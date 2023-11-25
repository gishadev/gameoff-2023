using System;
using System.Collections.Generic;
using System.Linq;
using gameoff.Core;
using gameoff.SavingLoading;
using UnityEngine;
using Zenject;

namespace gameoff.PlayerManager
{
    public class PlayerUpgradesController : IPlayerUpgradesController
    {
        [Inject] private ISaveLoadController _saveLoadController;
        [Inject] private GameDataSO _gameDataSO;

        public event Action<UpgradeDataSO[]> UpgradesShowed;

        public Func<bool> UpgradesCanBeShown => () =>
        {
            return _gameDataSO.UpgradesPack
                .Any(x => x.TargetLevelNumber == GameManager.CurrentLevelNumber &&
                          _saveLoadController.CurrentSaveData.CompletedLevelsCount < GameManager.CurrentLevelNumber);
        };

        public List<UpgradeEnumType> UnlockedUpgrades { get; } = new();

        private UpgradeDataSO[] _allUpgrades;

        public void Init()
        {
            _allUpgrades = Helpers.FindScriptableObjects<UpgradeDataSO>(Constants.RESOURCES_UPGRADES_PATH).ToArray();

            UnlockedUpgrades.AddRange(_saveLoadController.CurrentSaveData.Upgrades.Select(x => (UpgradeEnumType) x));
        }

        public void Dispose()
        {
        }

        public void Upgrade(UpgradeEnumType upgradeEnumType)
        {
            if (UnlockedUpgrades.Contains(upgradeEnumType))
            {
                Debug.Log("Upgrade already exists.");
                return;
            }

            Debug.Log($"Upgrade acquired {upgradeEnumType.ToString()}");
            UnlockedUpgrades.Add(upgradeEnumType);

            _saveLoadController.CurrentSaveData.Upgrades.Add((int) upgradeEnumType);
            _saveLoadController.SaveGame();
        }

        public void ShowUpgrades()
        {
            var upgradeEnumsToShow =
                _gameDataSO.UpgradesPack.FirstOrDefault(x => x.TargetLevelNumber == GameManager.CurrentLevelNumber)
                    ?.Upgrades;

            upgradeEnumsToShow =
                upgradeEnumsToShow
                    .Where(x => !_saveLoadController.CurrentSaveData.Upgrades.Contains((int) x.UpgradeEnumType))
                    .ToArray();

            var upgradesToShow = _allUpgrades
                .Where(x => upgradeEnumsToShow.Contains(x))
                .ToArray();

            UpgradesShowed?.Invoke(upgradesToShow);
        }
    }
}