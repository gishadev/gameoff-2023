using gameoff.Core;
using gameoff.SavingLoading;
using gishadev.tools.Audio;
using gishadev.tools.SceneLoading;
using gishadev.tools.UI;
using UnityEngine;
using Zenject;

namespace gameoff.UI.MainMenu
{
    public class MainMenuController : MenuController
    {
        [SerializeField] private Page levelsPage, settingsPage, creditsPage;

        [Inject] private ISaveLoadController _saveLoadController;
        [Inject] private GameDataSO _gameData;

        protected override void Awake()
        {
            base.Awake();
            if (_saveLoadController.CurrentSaveData == null)
                _saveLoadController.LoadGame();

            AudioManager.I.PlayAudio(MusicAudioEnum.MENU_MUSIC);
        }

        protected override void Start()
        {
            base.Start();

            AudioManager.I.SetMusicVolume(_gameData.MusicVolumeEvent.Value);
            AudioManager.I.SetSFXVolume(_gameData.SFXVolumeEvent.Value);
        }

        private void OnEnable()
        {
            _gameData.MusicVolumeEvent.ChangedValue += OnMusicVolumeChanged;
            _gameData.SFXVolumeEvent.ChangedValue += OnSFXVolumeChanged;
        }

        private void OnDisable()
        {
            _gameData.MusicVolumeEvent.ChangedValue -= OnMusicVolumeChanged;
            _gameData.SFXVolumeEvent.ChangedValue -= OnSFXVolumeChanged;
        }

        private void OnSFXVolumeChanged(float volume) => AudioManager.I.SetSFXVolume(volume);
        private void OnMusicVolumeChanged(float volume) => AudioManager.I.SetMusicVolume(volume);

        public static void OnPlayClicked()
        {
            SceneLoader.I.AsyncSceneLoad(Constants.GAME_SCENE_NAME);
            AudioManager.I.PlayAudio(SFXAudioEnum.CLICK);
        }

        public void OnQuitClicked()
        {
            Application.Quit();
            AudioManager.I.PlayAudio(SFXAudioEnum.CLICK);
        }

        public void OnLevelsPageClicked()
        {
            PushPage(levelsPage);
            AudioManager.I.PlayAudio(SFXAudioEnum.CLICK);
        }
        public void OnCreditsPageClicked()
        {
            PushPage(creditsPage);
            AudioManager.I.PlayAudio(SFXAudioEnum.CLICK);
        }

        public void OnSettingsPageClicked()
        {
            PushPage(settingsPage);

            AudioManager.I.PlayAudio(SFXAudioEnum.CLICK);
        }

        public void OnResetClicked()
        {
            _saveLoadController.ResetAndSave();
            _saveLoadController.LoadGame();

            AudioManager.I.PlayAudio(SFXAudioEnum.CLICK);
        }

        public void OnMainPageClicked()
        {
            PopPage();
            AudioManager.I.PlayAudio(SFXAudioEnum.CANCEL);
        }
    }
}