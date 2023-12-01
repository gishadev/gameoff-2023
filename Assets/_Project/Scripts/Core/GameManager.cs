using System;
using System.Linq;
using gameoff.Enemy;
using gameoff.PlayerManager;
using gameoff.SavingLoading;
using gameoff.World;
using gishadev.tools.Audio;
using gishadev.tools.SceneLoading;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace gameoff.Core
{
    public class GameManager : MonoBehaviour
    {
        [Inject] private ISaveLoadController _saveLoadController;
        [Inject] private DiContainer _diContainer;
        private static GameDataSO _gameDataSO;
        public static bool IsPaused { private set; get; }

        [ShowInInspector] public static int CurrentLevelNumber { get; private set; } = 1;
        public static event Action<bool> PauseChanged;
        public static event Action Won;
        public static event Action Lost;

        // Boolean to remove the ability to enter into pause mode when player is lost or won.
        private static bool _pauseBlocked;

        private CustomInput _customInput;
        private LevelLoader _levelLoader;

        private void Awake()
        {
            _levelLoader = new LevelLoader(_diContainer, null);
            _levelLoader.LoadLevel(CurrentLevelNumber - 1);

            _customInput = new CustomInput();
            _pauseBlocked = false;

            _gameDataSO = _diContainer.Resolve<GameDataSO>();

            PlayLevelMusic();
        }


        private void OnEnable()
        {
            _customInput.Enable();
            _customInput.General.Pause.performed += OnPausePerformed;
            Hive.Died += OnHiveDied;
            Player.Died += OnPlayerDied;
            HumanBase.CreepReachedBase += OnCreepReachedBase;
        }

        private void OnDisable()
        {
            _customInput.General.Pause.performed -= OnPausePerformed;
            Hive.Died -= OnHiveDied;
            Player.Died -= OnPlayerDied;
            HumanBase.CreepReachedBase -= OnCreepReachedBase;

            _customInput.Disable();
        }

        public static void SetCurrentLevel(int levelNumber) => CurrentLevelNumber = levelNumber;

        [HorizontalGroup("Split1")]
        [Button("Pause")]
        private static void PauseGame()
        {
            if (_pauseBlocked)
                return;

            IsPaused = true;
            PauseChanged?.Invoke(IsPaused);
            Time.timeScale = 0f;
        }

        [HorizontalGroup("Split1")]
        [Button("Resume")]
        public static void ResumeGame()
        {
            IsPaused = false;
            PauseChanged?.Invoke(IsPaused);
            Time.timeScale = 1f;
        }

        [HorizontalGroup("Split1")]
        [Button("Restart")]
        public static void RestartGame()
        {
            ResumeGame();
            SceneLoader.I.AsyncSceneLoad(Constants.GAME_SCENE_NAME);
        }

        [HorizontalGroup("Split1")]
        [Button("NextLevel")]
        public static void NextLevel()
        {
            if (CurrentLevelNumber + 1 > _gameDataSO.Levels.Length)
            {
                ResumeGame();
                SceneLoader.I.AsyncSceneLoad(Constants.WIN_SCENE_NAME);
                return;
            }

            CurrentLevelNumber++;
            ResumeGame();
            SceneLoader.I.AsyncSceneLoad(Constants.GAME_SCENE_NAME);
        }

        [HorizontalGroup("Split2")]
        [Button(ButtonSizes.Large), GUIColor("green")]
        private void Win()
        {
            Debug.Log("Win");
            _pauseBlocked = true;
            Won?.Invoke();
            Time.timeScale = 0f;

            if (CurrentLevelNumber > _saveLoadController.CurrentSaveData.CompletedLevelsCount)
            {
                _saveLoadController.CurrentSaveData.CompletedLevelsCount = CurrentLevelNumber;
                _saveLoadController.SaveGame();
            }
        }

        [HorizontalGroup("Split2")]
        [Button(ButtonSizes.Large), GUIColor("red")]
        private void Lose()
        {
            Debug.Log("Lose");
            _pauseBlocked = true;
            Lost?.Invoke();
            Time.timeScale = 0f;
        }

        private void PlayLevelMusic()
        {
            int difficulty = _gameDataSO.Levels[CurrentLevelNumber - 1].Difficulty;
            MusicAudioEnum gameMusicEnum;
            switch (difficulty)
            {
                case 1:
                    gameMusicEnum = MusicAudioEnum.GAME_MUSIC_1;
                    break;
                case 2:
                    gameMusicEnum = MusicAudioEnum.GAME_MUSIC_2;
                    break;
                default:
                    gameMusicEnum = MusicAudioEnum.GAME_MUSIC_3;
                    break;
            }

            AudioManager.I.PlayAudio(gameMusicEnum);
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
        private void OnCreepReachedBase() => Lose();
    }
}