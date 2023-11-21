﻿using gameoff.Core;
using gishadev.tools.SceneLoading;
using gishadev.tools.UI;
using UnityEngine;

namespace gameoff.UI.Game
{
    public class GameMenuController : MenuController
    {
        [SerializeField] private Page pausePopup, winPopup, losePopup;

        private void OnEnable()
        {
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

        public void OnRestartClicked()
        {
            GameManager.RestartGame();
        }

        public void OnResumeClicked()
        {
            PopPage();
            GameManager.ResumeGame();
        }

        public void OnMainMenuClicked()
        {
            GameManager.ResumeGame();
            SceneLoader.I.AsyncSceneLoad(Constants.MAIN_MENU_SCENE_NAME);
        }

        private void OnLost()
        {
            PushPage(losePopup);
        }

        private void OnWon()
        {
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