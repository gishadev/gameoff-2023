﻿using UnityEngine;

namespace gameoff.PlayerManager
{
    [CreateAssetMenu(fileName = "DashAbilityData", menuName = "ScriptableObjects/DashAbilityDataSO")]
    public class DashAbilityDataSO : AbilityDataSO
    {
        [field: SerializeField] public float DashingPower { get; private set; } = 10f;
        [field: SerializeField] public float DashingTime { get; private set; } = 0.4f;
    }
}