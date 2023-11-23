﻿using System;
using System.Collections.Generic;

namespace gameoff.SavingLoading
{
    [Serializable]
    public class GameSaveData
    {
        public List<int> Upgrades { get; set; }

        public GameSaveData()
        {
            Upgrades = new List<int>();
        }
    }
}