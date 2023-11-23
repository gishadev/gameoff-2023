using UnityEngine;

namespace gameoff.PlayerManager
{
    public abstract class AbilityDataSO : UpgradeDataSO
    {
        [field: SerializeField] public float AbilityCooldown { private set; get; } = 1f;
    }
}