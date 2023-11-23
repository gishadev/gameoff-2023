using System;
using System.Collections.Generic;
using System.Linq;
using gameoff.Core;
using UnityEngine;

namespace gameoff.PlayerManager
{
    public class PlayerUpgradesController : IPlayerUpgradesController
    {
        public event Action<UpgradeDataSO[]> UpgradesShowed;

        public Func<bool> UpgradesCanBeShown => () => { return true; };

        public List<UpgradeEnumType> UnlockedUpgrades { get; } = new();

        private UpgradeDataSO[] _allUpgrades;

        public void Init()
        {
            _allUpgrades = Helpers.FindScriptableObjects<UpgradeDataSO>(Constants.RESOURCES_UPGRADES_PATH).ToArray();

            // // TODO: Load abilities
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
        }

        public void ShowUpgrades()
        {
            var upgradeEnumsToShow = new[]
                {UpgradeEnumType.ABILITY_DASH, UpgradeEnumType.ABILITY_EXPLOSION};

            var upgradesToShow = _allUpgrades
                .Where(x => upgradeEnumsToShow.Contains(x.UpgradeEnumType))
                .ToArray();

            UpgradesShowed?.Invoke(upgradesToShow);
        }
    }
}