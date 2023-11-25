using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace gameoff.PlayerManager
{
    [Serializable]
    public class UpgradesPack
    {
        [SerializeField] private int targetLevelNumber;
        [SerializeField, HorizontalGroup] private UpgradeDataSO[] upgrades;

        public UpgradeDataSO[] Upgrades => upgrades;

        public int TargetLevelNumber
        {
            get => targetLevelNumber;
            set => targetLevelNumber = value;
        }
    }
}