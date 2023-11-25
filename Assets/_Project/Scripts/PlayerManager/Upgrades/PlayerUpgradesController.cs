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


        public void Init()
        {
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
            // Getting upgrades from current and previous upgrade packs.
            var upgradePacks =
                _gameDataSO.UpgradesPack
                    .Where(x => x.TargetLevelNumber <= GameManager.CurrentLevelNumber)
                    .ToArray();

            var upgradesToShow = new List<UpgradeDataSO>();
            foreach (var pack in upgradePacks)
            foreach (var upgrade in pack.Upgrades)
                if (!upgradesToShow.Contains(upgrade))
                    upgradesToShow.Add(upgrade);

            upgradesToShow = upgradesToShow
                .Where(x => !_saveLoadController.CurrentSaveData.Upgrades.Contains((int) x.UpgradeEnumType))
                .ToList();

            UpgradesShowed?.Invoke(upgradesToShow.ToArray());
        }
    }
}