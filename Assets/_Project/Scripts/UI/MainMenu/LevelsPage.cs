using System;
using System.Collections;
using System.Linq;
using gameoff.SavingLoading;
using gameoff.World;
using gishadev.tools.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

namespace gameoff.UI.MainMenu
{
    public class LevelsPage : Page
    {
        [SerializeField] private Transform levelGUIParent;

        [SerializeField, BoxGroup("Level Info Box")]
        private TMP_Text levelTitle;

        [SerializeField, BoxGroup("Level Info Box")]
        private TMP_Text levelStatusLabel;

        [SerializeField, BoxGroup("Level Info Box")]
        private Transform skullsParent;
        
        [SerializeField, BoxGroup("Level Info Box")]
        private GameObject levelInfoBox;

        [Inject] private ISaveLoadController _saveLoadController;

        private LevelGUI[] _levelGUIs;

        private void OnEnable()
        {
            LevelGUI.PointerEnter += OnLevelPointerEnter;
            LevelGUI.PointerExit += OnLevelPointerExit;

            _levelGUIs = levelGUIParent.GetComponentsInChildren<LevelGUI>()
                .OrderBy(x => x.LevelData.LevelOrder)
                .ToArray();

            foreach (var level in _levelGUIs)
                level.SetClosed();

            var completedLevels = _saveLoadController.CurrentSaveData.CompletedLevelsCount;
            for (int i = 0; i < _levelGUIs.Length && i < completedLevels + 1; i++)
            {
                if (IsCleared(_levelGUIs[i].LevelData))
                    _levelGUIs[i].SetCompleted();
                else if (IsInfected(_levelGUIs[i].LevelData))
                    _levelGUIs[i].SetInfected();
            }
        }

        private void OnDisable()
        {
            LevelGUI.PointerEnter -= OnLevelPointerEnter;
            LevelGUI.PointerExit -= OnLevelPointerExit;
        }

        private void OnLevelPointerEnter(LevelDataSO levelData)
        {
            levelTitle.text = $"Sector {levelData.LevelOrder}: {levelData.LevelName}";

            if (IsCleared(levelData))
                levelStatusLabel.text = $"Status: Cleared";
            else if (IsInfected(levelData))
                levelStatusLabel.text = $"Status: Infected";
            else
                levelStatusLabel.text = $"Status: Closed";

            for (int i = 0; i < skullsParent.childCount; i++)
                skullsParent.GetChild(i).gameObject.SetActive(i < levelData.Difficulty);

            levelInfoBox.SetActive(true);
        }
        
        private void OnLevelPointerExit()
        {
            levelInfoBox.SetActive(false);
        }

        public bool IsCleared(LevelDataSO levelData)
        {
            var completedLevels = _saveLoadController.CurrentSaveData.CompletedLevelsCount;
            return levelData.LevelOrder <= completedLevels;
        }

        public bool IsInfected(LevelDataSO levelData)
        {
            var completedLevels = _saveLoadController.CurrentSaveData.CompletedLevelsCount;
            return levelData.LevelOrder == completedLevels + 1;
        }
    }
}