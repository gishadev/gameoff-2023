using System;
using System.Collections.Generic;
using gameoff.Core;
using gishadev.tools.Events;
using UnityEngine;
using Zenject;

namespace gameoff.PlayerManager
{
    public class PlayerUpgradesController : IPlayerUpgradesController
    {
        [Inject] private GameDataSO _gameDataSO;

        public List<AbilityEnumType> UnlockedAbilities { get; } = new();

        public void Init()
        {
            _gameDataSO.UnlockEventChannel.ChangedValue += OnUnlockChannel;
            // TODO: Load abilities
            UnlockAbility(AbilityEnumType.ABILITY_DASH);
            UnlockAbility(AbilityEnumType.ABILITY_EXPLOSION);
        }

        public void Dispose()
        {
            _gameDataSO.UnlockEventChannel.ChangedValue -= OnUnlockChannel;
        }

        private void UnlockAbility(AbilityEnumType abilityEnumType)
        {
            if (UnlockedAbilities.Contains(abilityEnumType))
            {
                Debug.Log("Ability already exists.");
                return;
            }

            UnlockedAbilities.Add(abilityEnumType);
        }

        private void Upgrade(IUpgradeable upgradeable)
        {
        }

        private void OnUnlockChannel(StringWrapper wrapper)
        {
            if (wrapper.value == "Dash")
                UnlockAbility(AbilityEnumType.ABILITY_DASH);
            if (wrapper.value == "Explosion")
                UnlockAbility(AbilityEnumType.ABILITY_EXPLOSION);
        }
    }

    public interface IUpgradeable
    {
        public void OnUpgrade();
    }

    public enum AbilityEnumType
    {
        ABILITY_DASH,
        ABILITY_EXPLOSION
    }
}