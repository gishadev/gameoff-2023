using gameoff.PlayerManager;
using gameoff.SavingLoading;
using UnityEngine;
using Zenject;

namespace gameoff.UI.Game
{
    public class AbilityCooldownHandler : CooldownHandler
    {
        [SerializeField] private AbilityDataSO targetToListen;

        [Inject] private ISaveLoadController _saveLoadController;

        protected void Start()
        {
            if (!_saveLoadController.CurrentSaveData.Upgrades.Contains((int) targetToListen.UpgradeEnumType))
                gameObject.SetActive(false);
        }

        private void OnEnable() => IAbility.Used += OnAbilityUsed;
        private void OnDisable() => IAbility.Used -= OnAbilityUsed;

        private void OnAbilityUsed(IAbility ability)
        {
            if (ability.AbilityDataSO != targetToListen)
                return;

            ShowCooldown(targetToListen.AbilityCooldown);
        }
    }
}