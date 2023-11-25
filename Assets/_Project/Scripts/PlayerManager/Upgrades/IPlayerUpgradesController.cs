﻿using System;
using System.Collections.Generic;

namespace gameoff.PlayerManager
{
    public interface IPlayerUpgradesController
    {
        event Action<UpgradeDataSO[]> UpgradesShowed;
        Func<bool> UpgradesCanBeShown { get; }
        void ShowUpgrades();
        
         void Upgrade(UpgradeEnumType upgradeEnumType);
         
        List<UpgradeEnumType> UnlockedUpgrades { get; }
        bool IsUnlocked(UpgradeEnumType upgradeToCheck);
        void Init();
        void Dispose();
    }
}