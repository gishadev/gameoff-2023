using System;
using gameoff.Enemy.SOs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace gameoff.Enemy
{
    [Serializable]
    public class EnemySpawnSettings
    {
        [field: SerializeField] public EnemyDataSO EnemyDataSO { private set; get; }
        [field: SerializeField] public int MaxCount { private set; get; } = 10;
        [field: SerializeField] public int MinSpawnCount { private set; get; } = 1;
        [field: SerializeField] public int MaxSpawnCount { private set; get; } = 3;
        [field: SerializeField] public float SpawnChance { private set; get; } = 0.1f;
        
        private bool CanOnlyBeEnemyEnum(OtherPoolEnum poolEnum, ref string errorMessage)
        {
            if (poolEnum.ToString().Contains("ENEMY"))
                return true;
            return false;
        }
    }
}