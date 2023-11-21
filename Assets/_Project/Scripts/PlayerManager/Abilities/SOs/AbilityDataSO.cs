using UnityEngine;

namespace gameoff.PlayerManager.SOs
{
    public abstract class AbilityDataSO : ScriptableObject
    {
        [field: SerializeField] public float AbilityCooldown { private set; get; } = 1f;
    }
}