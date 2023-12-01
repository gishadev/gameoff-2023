using System;
using gameoff.Core;
using Newtonsoft.Json;
using UnityEngine;

namespace gameoff.SavingLoading
{
    public class SaveLoadController : ISaveLoadController
    {
        public GameSaveData CurrentSaveData { get; private set; }
        public event Action<GameSaveData> GameDataLoaded;

        public void SaveGame()
        {
            Debug.Log("Game Saved");

            if (CurrentSaveData == null)
                CurrentSaveData = new GameSaveData();
            
            string json = JsonConvert.SerializeObject(CurrentSaveData);

            PlayerPrefs.SetString(Constants.SAVE_FILE_STRING, json);
            PlayerPrefs.Save();
        }

        public GameSaveData LoadGame()
        {
            Debug.Log("Game Loaded");

            if (PlayerPrefs.HasKey(Constants.SAVE_FILE_STRING))
            {
                var json = PlayerPrefs.GetString(Constants.SAVE_FILE_STRING);
                CurrentSaveData = JsonConvert.DeserializeObject<GameSaveData>(json);
            }
            else
                CurrentSaveData = new GameSaveData();

            GameDataLoaded?.Invoke(CurrentSaveData);
            return CurrentSaveData;
        }

        public void ResetAndSave()
        {
            CurrentSaveData = new GameSaveData();
            SaveGame();
        }
    }
}