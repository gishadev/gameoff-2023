using System;
using gameoff.Core;
using gameoff.World;
using gishadev.tools.SceneLoading;
using gishadev.tools.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace gameoff.UI.MainMenu
{
    public class MainMenuController : MenuController
    {
        [SerializeField, BoxGroup("Level Box")]
        private GameObject levelInfoBox;

        [SerializeField, BoxGroup("Level Box")]
        private TMP_Text levelTitle;

        [SerializeField, BoxGroup("Level Box")]
        private TMP_Text levelStatusLabel;

        [SerializeField, BoxGroup("Level Box")]
        private Transform skullsParent;

        private void OnEnable()
        {
            LevelGUI.PointerEnter += OnLevelPointerEnter;
            LevelGUI.PointerExit += OnLevelPointerExit;
        }

        private void OnDisable()
        {
            LevelGUI.PointerEnter -= OnLevelPointerEnter;
            LevelGUI.PointerExit -= OnLevelPointerExit;
        }

        public void OnPlayClicked() => SceneLoader.I.AsyncSceneLoad(Constants.GAME_SCENE_NAME);
        public void OnQuitClicked() => Application.Quit();

        private void OnLevelPointerEnter(LevelDataSO levelData)
        {
            levelTitle.text = $"Sector {levelData.LevelIndex + 1}: {levelData.LevelName}";
            levelStatusLabel.text = $"Status: Infected";

            for (int i = 0; i < skullsParent.childCount; i++) 
                skullsParent.GetChild(i).gameObject.SetActive(i < levelData.Difficulty);

            levelInfoBox.SetActive(true);
        }
        
        private void OnLevelPointerExit()
        {
            levelInfoBox.SetActive(false);
        }
    }
}