using UnityEngine;

namespace gameoff.PlayerManager
{
    [CreateAssetMenu(fileName = "ExplosionAbilityData", menuName = "ScriptableObjects/ExplosionAbilityDataSO")]
    public class ExplosionAbilityDataSO : AbilityDataSO
    {
        [field: SerializeField] public int ProjectileDamage { private set; get; } = 5;
        [field: SerializeField] public int ProjectileCount { private set; get; } = 10;

        [field: SerializeField] public int ClearIterations { private set; get; } = 10;
        [field: SerializeField] public float ClearMaxRadius { private set; get; } = 5f;
        [field: SerializeField] public float FullClearExpandingTime { private set; get; } = 1f;
    }
}