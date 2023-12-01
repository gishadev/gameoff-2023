﻿using gameoff.Core;
using gameoff.PlayerManager;
using gameoff.SavingLoading;
using gishadev.tools.SceneLoading;
using gishadev.tools.UI;
using UnityEngine;
using Zenject;

namespace gameoff.UI.Game
{
    public class GameMenuController : MenuController
    {
        [SerializeField] private Page pausePopup, winPopup, losePopup, upgradesPopup;

        [Inject] private IPlayerUpgradesController _playerUpgradesController;
        [Inject] private ISaveLoadController _saveLoadController;

        private bool _blockPopupShowing;
        
        private void OnEnable()
        {
            _blockPopupShowing = false;
            GameManager.PauseChanged += OnPauseChanged;
            GameManager.Won += OnWon;
            GameManager.Lost += OnLost;
        }

        private void OnDisable()
        {
            GameManager.PauseChanged -= OnPauseChanged;
            GameManager.Won -= OnWon;
            GameManager.Lost -= OnLost;
        }

        public void OnRestartClicked() => GameManager.RestartGame();

        public void OnResumeClicked()
        {
            PopPage();
            GameManager.ResumeGame();
        }

        public void OnContinueClicked()
        {
            GameManager.NextLevel();
            _saveLoadController.SaveGame();
        }
        
        public void OnMainMenuClicked()
        {
            GameManager.ResumeGame();
            SceneLoader.I.AsyncSceneLoad(Constants.MAIN_MENU_SCENE_NAME);
            _saveLoadController.SaveGame();
        }

        private void OnLost()
        {
            if (_blockPopupShowing)
                return;
            _blockPopupShowing = true;
            
            PushPage(losePopup);
        }

        private void OnWon()
        {
            if (_blockPopupShowing)
                return;
            _blockPopupShowing = true;
            
            if (_playerUpgradesController.UpgradesCanBeShown())
            {
                PushPage(upgradesPopup);
                _playerUpgradesController.ShowUpgrades();
            }
            else
                PushPage(winPopup);
        }

        private void OnPauseChanged(bool pauseValue)
        {
            if (pauseValue)
                PushPage(pausePopup);
            else
                PopPage();
        }
    }
}