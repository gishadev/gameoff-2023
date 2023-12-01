using System.Linq;
using gameoff.Core;
using gameoff.SavingLoading;
using gameoff.World;
using gishadev.tools.Audio;
using gishadev.tools.SceneLoading;
using gishadev.tools.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

namespace gameoff.UI.MainMenu
{
    public class MainMenuController : MenuController
    {
        [SerializeField] private Transform levelGUIParent;

        [SerializeField, BoxGroup("Level Info Box")]
        private GameObject levelInfoBox;

        [SerializeField, BoxGroup("Level Info Box")]
        private TMP_Text levelTitle;

        [SerializeField, BoxGroup("Level Info Box")]
        private TMP_Text levelStatusLabel;

        [SerializeField, BoxGroup("Level Info Box")]
        private Transform skullsParent;

        [Inject] private ISaveLoadController _saveLoadController;

        private LevelGUI[] _levelGUIs;

        protected override void Awake()
        {
            base.Awake();
            if (_saveLoadController.CurrentSaveData == null)
                _saveLoadController.LoadGame();

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
            
            AudioManager.I.PlayAudio(MusicAudioEnum.MENU_MUSIC);
        }

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

        public static void OnPlayClicked() => SceneLoader.I.AsyncSceneLoad(Constants.GAME_SCENE_NAME);
        public void OnQuitClicked() => Application.Quit();

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