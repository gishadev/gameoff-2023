using System;
using System.Collections.Generic;

namespace gameoff.PlayerManager
{
    public interface IPlayerUpgradesController
    {
        List<AbilityEnumType> UnlockedAbilities { get; }
        void Init();
        void Dispose();
    }
}