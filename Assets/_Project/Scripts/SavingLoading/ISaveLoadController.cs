﻿using System;

namespace gameoff.SavingLoading
{
    public interface ISaveLoadController
    {
        GameSaveData CurrentSaveData { get; }
        event Action<GameSaveData> GameDataLoaded;
        void SaveGame();
        GameSaveData LoadGame();
    }
}