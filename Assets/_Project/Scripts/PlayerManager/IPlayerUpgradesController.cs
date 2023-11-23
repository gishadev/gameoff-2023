using System;
using System.Collections.Generic;

namespace gameoff.PlayerManager
{
    public interface IPlayerUpgradesController
    {
        event Action<UpgradeDataSO[]> UpgradesShowed;
        Func<bool> UpgradesCanBeShown { get; }
        void ShowUpgrades();
        
        List<UpgradeEnumType> UnlockedAbilities { get; }
        void Init();
        void Dispose();
    }
}