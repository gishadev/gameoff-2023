using System;
using System.Collections.Generic;
using System.Linq;
using gameoff.Core;
using gishadev.tools.Events;
using UnityEngine;
using Zenject;

namespace gameoff.PlayerManager
{
    public class PlayerUpgradesController : IPlayerUpgradesController
    {
        [Inject] private GameDataSO _gameDataSO;

        public event Action<UpgradeDataSO[]> UpgradesShowed;

        public Func<bool> UpgradesCanBeShown => () => { return true; };


        public List<UpgradeEnumType> UnlockedAbilities { get; } = new();

        private UpgradeDataSO[] _allUpgrades;

        public void Init()
        {
            _gameDataSO.UnlockEventChannel.ChangedValue += OnUnlockChannel;

            _allUpgrades = Helpers.FindScriptableObjects<UpgradeDataSO>(Constants.RESOURCES_UPGRADES_PATH).ToArray();

            // // TODO: Load abilities
            // UnlockAbility(UpgradeEnumType.ABILITY_DASH);
            // UnlockAbility(UpgradeEnumType.ABILITY_EXPLOSION);
        }

        public void Dispose()
        {
            _gameDataSO.UnlockEventChannel.ChangedValue -= OnUnlockChannel;
        }

        private void UnlockAbility(UpgradeEnumType upgradeEnumType)
        {
            if (UnlockedAbilities.Contains(upgradeEnumType))
            {
                Debug.Log("Ability already exists.");
                return;
            }

            UnlockedAbilities.Add(upgradeEnumType);
        }

        private void Upgrade(IUpgradeable upgradeable)
        {
        }

        private void OnUnlockChannel(StringWrapper wrapper)
        {
            if (wrapper.value == "Dash")
                UnlockAbility(UpgradeEnumType.ABILITY_DASH);
            if (wrapper.value == "Explosion")
                UnlockAbility(UpgradeEnumType.ABILITY_EXPLOSION);
        }

        public void ShowUpgrades()
        {
            var upgradeEnumsToShow = new[]
                {UpgradeEnumType.ABILITY_DASH, UpgradeEnumType.ABILITY_EXPLOSION};

            var upgradesToShow = _allUpgrades
                .Where(x => upgradeEnumsToShow.Contains(x.UpgradeType))
                .ToArray();

            UpgradesShowed?.Invoke(upgradesToShow);
        }
    }

    public interface IUpgradeable
    {
        public void OnUpgrade();
    }

    public enum UpgradeEnumType
    {
        ABILITY_DASH,
        ABILITY_EXPLOSION
    }
}