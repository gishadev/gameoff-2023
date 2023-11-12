using System;
using gameoff.Core;
using gishadev.tools.UI;
using UnityEngine;
using UnityEngine.InputSystem;

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