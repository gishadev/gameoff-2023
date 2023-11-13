using System;
using System.Linq;
using gameoff.Enemy;
using gameoff.PlayerManager;
using gishadev.tools.Effects;
using gishadev.tools.SceneLoading;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace gameoff.Core
{
    public class GameManager : MonoBehaviour
    {
        public static bool IsPaused { private set; get; }
        public static event Action<bool> PauseChanged;
        public static event Action Won;
        public static event Action Lost;

        // Boolean to remove the ability to enter into pause mode when player is lost or won.
        private static bool _pauseBlocked;

        private CustomInput _customInput;

        private void Awake()
        {
            _customInput = new CustomInput();
            _pauseBlocked = false;
        }

        private void OnEnable()
        {
            _customInput.Enable();
            _customInput.General.Pause.performed += OnPausePerformed;
            Hive.Died += OnHiveDied;
            Player.Died += OnPlayerDied;
        }

        private void OnDisable()
        {
            _customInput.General.Pause.performed -= OnPausePerformed;
            Hive.Died -= OnHiveDied;
            Player.Died -= OnPlayerDied;

            _customInput.Disable();
        }


        public static void RestartGame()
        {
            SceneLoader.I.AsyncSceneLoad(Constants.GAME_SCENE_NAME);
        }

        private static void PauseGame()
        {
            if (_pauseBlocked)
                return;

            IsPaused = true;
            PauseChanged?.Invoke(IsPaused);
            Time.timeScale = 0f;
        }

        public static void ResumeGame()
        {
            IsPaused = false;
            PauseChanged?.Invoke(IsPaused);
            Time.timeScale = 1f;
        }

        private void Lose()
        {
            Debug.Log("Lose");
            _pauseBlocked = true;
            Lost?.Invoke();
        }

        private void Win()
        {
            Debug.Log("Win");
            _pauseBlocked = false;
            Won?.Invoke();
        }


        private void OnPausePerformed(InputAction.CallbackContext value)
        {
            if (IsPaused)
                ResumeGame();
            else
                PauseGame();
        }

        private void OnHiveDied(Hive hive)
        {
            var hives = FindObjectsOfType<Hive>().Where(x => x != hive).ToArray();
            if (hives.Length <= 0)
                Win();
        }

        private void OnPlayerDied() => Lose();
    }
}